
using Microsoft.Owin;
using SymphonySofttech.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using VATViewModel.DTOs;
using VATWebAPI.Models;
using WebAPI.Repo;

namespace VATWebAPI.Security
{
    public class AuthHelper
    {
        public void Authenticate(LogInModel user)
        {
            UserInformationRepo _userRepo = new UserInformationRepo();
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
                sysInfo.DatabaseName; //Converter.DESDecrypt(DBConstant.PassPhrase, DBConstant.EnKey, sysInfo.DatabaseName);

            List<UserInformationVM> vms = _userRepo.SelectForLogin(new LoginVM()
            {
                DatabaseName = dbName,
                UserName = user.UserName,
                UserPassword = user.Password
            });

            if (!vms.Any())
            {
                throw new Exception("Wrong UserName/Password");
            }

            _commonRepo.LoginSuccess(dbName);

            ReportDSRepo reportDsdal = new ReportDSRepo();
            DataSet ReportResult = reportDsdal.ComapnyProfileString(sysInfo.CompanyID, vms[0].UserID);

            settingVM.SettingsDT = ReportResult.Tables[2];
            settingVM.SettingsDTUser = ReportResult.Tables[3];
            settingVM.UserInfoDT = ReportResult.Tables[4];


            

        }



    }
}