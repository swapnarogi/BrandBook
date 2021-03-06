﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using BrandBookModel;
namespace BrandBookDBContext
{
    public class DBUser:DBContext
    {
         #region private property

        private SqlDataReader _dataReader;
        private SqlParameter[] _spParameters;
        private DataTable _dataTable;
        private DataSet _dataSet;
        private string _spName;

        #endregion private property

        #region Constructor

        // default constructor
        public DBUser()
        {
            this._dataReader = null;
            this._spParameters = null;
            this._dataTable = null;
            this._dataSet = null;
            this._spName = null;
        }

        #endregion Constructor

        #region DBUser public method

        public DataTable GetUserDetails(UserModel userModel)
        {
            _spName = "UserDetails_getUserDetails";
            _dataTable = new DataTable();
            _spParameters = new SqlParameter[]{
                new SqlParameter("@UserId",userModel.UserID)
						};
            _dataTable = ExecuteDataTable(_spName, _spParameters);
            return _dataTable;
        }

        public UserModel SaveUserDetails(UserModel userModel)
        {
            _spName = "UserDetails_SaveUserDetails";
            SqlParameter sqlparam = new SqlParameter("@UserDetailsID", userModel.UserDetailsID);
            sqlparam.Direction = ParameterDirection.InputOutput;
            _spParameters = new SqlParameter[] 
            {
                new SqlParameter("@UserId",userModel.UserID),
                sqlparam,
                new SqlParameter("@FirsName",userModel.FirstName),
                new SqlParameter("@LasName",userModel.LastName),
                new SqlParameter("@Designation",userModel.Desination),
                new SqlParameter("@Gender",userModel.Gender),
                new SqlParameter("@proPicId",userModel.Gender=="Male"?1:2),
                new SqlParameter("@createdDate",DateTime.Now)
            };
            int result = ExecuteNoResult(_spName, _spParameters);
            userModel.UserDetailsID = Convert.ToInt32(sqlparam.Value);
            return userModel;
        }
        #endregion DBUser public method
    }
}
