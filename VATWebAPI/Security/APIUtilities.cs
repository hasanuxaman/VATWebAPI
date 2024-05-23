
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VATWebAPI.Models;
using WebAPI.Repo;
using SymphonySofttech.Utilities;
using System.Data;
using WebAPI.Models;
using System.Reflection;

namespace VATWebAPI.Security
{
    public class APIUtilities
    {
        public (bool isValid, string errorMessage) Validate(string userName, string password, string bin)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return (false, "Please provide a UserName for access");
            }

            if (string.IsNullOrEmpty(password))
            {
                return (false, "Please provide Password for access");
            }

            if (string.IsNullOrEmpty(bin))
            {
                return (false, "Please provide BIN for access");
            }

            // All validations passed
            return (true, null);
        }

        public string  GetDbName(LogInModel user)
        {
            var dbName = "";

            try
            {
                CommonRepo _commonRepo = new CommonRepo();

                SymphonyVATSysCompanyInformationRepo _sysComRepo = new SymphonyVATSysCompanyInformationRepo();
                _commonRepo.SuperInformationFileExist(AppDomain.CurrentDomain.BaseDirectory);

                var enBin = Converter.DESEncrypt(DBConstant.PassPhrase, DBConstant.EnKey, user.BIN.Trim());

                var sysInfo = _sysComRepo.SelectAll(null, new[] { "Bin" }, new[] { enBin }).FirstOrDefault();

                if (sysInfo == null)
                {
                    throw new Exception("BIN does not exist");
                }

                 dbName =
                    sysInfo.DatabaseName; //Converter.DESDecrypt(DBConstant.PassPhrase, DBConstant.EnKey, sysInfo.DatabaseName);

            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);

            }
            return dbName;

        }

        public void Login(LogInModel user)
        {
            try
            {
                SymphonyVATSysCompanyInformationRepo _sysComRepo = new SymphonyVATSysCompanyInformationRepo();
                CommonRepo _commonRepo = new CommonRepo();
                _commonRepo.SuperInformationFileExist(AppDomain.CurrentDomain.BaseDirectory);

                var enBin = Converter.DESEncrypt(DBConstant.PassPhrase, DBConstant.EnKey, user.BIN.Trim());

                var sysInfo = _sysComRepo.SelectAll(null, new[] { "Bin" }, new[] { enBin }).FirstOrDefault();

                if (sysInfo == null)
                {
                    throw new Exception("BIN does not exist");
                }

                var dbName =
                    sysInfo.DatabaseName; 

                _commonRepo.LoginSuccess(dbName);
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                
            }
        }



        public static DataTable SalesConvertToDataTable(List<SaleInvoice> saleInvoices)
        {
            DataTable dataTable = new DataTable("SaleInvoices");

            try
            {
                // Define the columns for the DataTable based on the Invoice class
                PropertyInfo[] invoiceProperties = typeof(Invoice).GetProperties();
                invoiceProperties = invoiceProperties.Take(invoiceProperties.Length - 1).ToArray();
                foreach (var property in invoiceProperties)
                {
                    dataTable.Columns.Add(property.Name, property.PropertyType);
                }

                // Define additional columns for the Item properties
                PropertyInfo[] itemProperties = typeof(SaleDetsils).GetProperties();
                foreach (var property in itemProperties)
                {
                    dataTable.Columns.Add(property.Name, property.PropertyType);
                }

                // Populate the DataTable with data from SaleInvoice objects and their Items
                if (saleInvoices != null)
                {
                    foreach (var saleInvoice in saleInvoices)
                    {
                        if (saleInvoice.Invoice.Items != null)
                        {
                            foreach (var item in saleInvoice.Invoice.Items)
                            {
                                DataRow row = dataTable.NewRow();

                                // Populate the row with data from Invoice properties
                                foreach (var property in invoiceProperties)
                                {
                                    row[property.Name] = property.GetValue(saleInvoice.Invoice);
                                }

                                // Populate the row with data from Item properties
                                foreach (var property in itemProperties)
                                {
                                    row[property.Name] = property.GetValue(item);
                                }

                                dataTable.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            return dataTable;
        }

        public static DataTable PurchaseConvertToDataTable(List<PurchasesInvoice> purchasesInvoices)
        {
            DataTable dataTable = new DataTable("PurchaseInvoices");

            try
            {
                // Define the columns for the DataTable based on the Invoice class
                PropertyInfo[] invoiceProperties = typeof(PurchaseInvoice).GetProperties();
                invoiceProperties = invoiceProperties.Take(invoiceProperties.Length - 1).ToArray();
                foreach (var property in invoiceProperties)
                {
                    dataTable.Columns.Add(property.Name, property.PropertyType);
                }

                // Define additional columns for the Item properties
                PropertyInfo[] itemProperties = typeof(PurchaseDetsils).GetProperties();
                foreach (var property in itemProperties)
                {
                    dataTable.Columns.Add(property.Name, property.PropertyType);
                }

                // Populate the DataTable with data from SaleInvoice objects and their Items
                if (purchasesInvoices != null)
                {
                    foreach (var purchasesInvoice in purchasesInvoices)
                    {
                        if (purchasesInvoice.Invoice.Items != null)
                        {
                            foreach (var item in purchasesInvoice.Invoice.Items)
                            {
                                DataRow row = dataTable.NewRow();

                                // Populate the row with data from Invoice properties
                                foreach (var property in invoiceProperties)
                                {
                                    row[property.Name] = property.GetValue(purchasesInvoice.Invoice);
                                }

                                // Populate the row with data from Item properties
                                foreach (var property in itemProperties)
                                {
                                    row[property.Name] = property.GetValue(item);
                                }

                                dataTable.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            return dataTable;
        }

        public static DataTable TransferConvertToDataTable(List<TransfersInvoice> transfersInvoices)
        {
            DataTable dataTable = new DataTable("TransferInvoices");

            try
            {
                // Define the columns for the DataTable based on the Invoice class
                PropertyInfo[] invoiceProperties = typeof(TransferInvoice).GetProperties();
                invoiceProperties = invoiceProperties.Take(invoiceProperties.Length - 1).ToArray();
                foreach (var property in invoiceProperties)
                {
                    dataTable.Columns.Add(property.Name, property.PropertyType);
                }

                // Define additional columns for the Item properties
                PropertyInfo[] itemProperties = typeof(TransferDetsils).GetProperties();
                foreach (var property in itemProperties)
                {
                    dataTable.Columns.Add(property.Name, property.PropertyType);
                }

                // Populate the DataTable with data from SaleInvoice objects and their Items
                if (transfersInvoices != null)
                {
                    foreach (var transferInvoice in transfersInvoices)
                    {
                        if (transferInvoice.Invoice.Items != null)
                        {
                            foreach (var item in transferInvoice.Invoice.Items)
                            {
                                DataRow row = dataTable.NewRow();

                                // Populate the row with data from Invoice properties
                                foreach (var property in invoiceProperties)
                                {
                                    row[property.Name] = property.GetValue(transferInvoice.Invoice);
                                }

                                // Populate the row with data from Item properties
                                foreach (var property in itemProperties)
                                {
                                    row[property.Name] = property.GetValue(item);
                                }

                                dataTable.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            return dataTable;
        }

        public static DataTable ProductionIssueConvertToDataTable(List<IssuesInvoice> issueInvoices)
        {
            DataTable dataTable = new DataTable("ProductionIssues");

            try
            {
                // Define the columns for the DataTable based on the Invoice class
                PropertyInfo[] invoiceProperties = typeof(IssueInvoice).GetProperties();
                invoiceProperties = invoiceProperties.Take(invoiceProperties.Length - 1).ToArray();
                foreach (var property in invoiceProperties)
                {
                    dataTable.Columns.Add(property.Name, property.PropertyType);
                }

                // Define additional columns for the Item properties
                PropertyInfo[] itemProperties = typeof(IssueDetsils).GetProperties();
                foreach (var property in itemProperties)
                {
                    dataTable.Columns.Add(property.Name, property.PropertyType);
                }

                // Populate the DataTable with data from SaleInvoice objects and their Items
                if (issueInvoices != null)
                {
                    foreach (var issueInvoice in issueInvoices)
                    {
                        if (issueInvoice.Invoice.Items != null)
                        {
                            foreach (var item in issueInvoice.Invoice.Items)
                            {
                                DataRow row = dataTable.NewRow();

                                // Populate the row with data from Invoice properties
                                foreach (var property in invoiceProperties)
                                {
                                    row[property.Name] = property.GetValue(issueInvoice.Invoice);
                                }

                                // Populate the row with data from Item properties
                                foreach (var property in itemProperties)
                                {
                                    row[property.Name] = property.GetValue(item);
                                }

                                dataTable.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            return dataTable;
        }




    }
}