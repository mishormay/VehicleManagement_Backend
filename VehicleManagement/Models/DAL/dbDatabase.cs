using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using VehicleManagement.Entities;
using VehicleManagement.ViewModels;

namespace VehicleManagement.Models.DAL
{
    public class dbDatabase
    {
        private readonly string _ConnectionString;
        public IConfiguration Configuration { get; }
        public dbDatabase(IConfiguration configuration)
        {
            Configuration = configuration;
            _ConnectionString = Configuration.GetConnectionString("UGADM");
        }

        //public ChartViewModel GetRegChartData()
        //{
        //    List<string> iLables = new List<string>();
        //    List<string> iDataValues = new List<string>();
        //    ChartViewModel model = new ChartViewModel();

        //    using (SqlConnection con = new SqlConnection(_ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("select * from vwLastSevenDaysReg", con);
        //        cmd.CommandType = CommandType.Text;
        //        con.Open();

        //        SqlDataReader sdr = cmd.ExecuteReader();


        //        while (sdr.Read())
        //        {
        //            DateTime dt = new DateTime();
        //            dt = Convert.ToDateTime(sdr["RegDate"]);
        //            iLables.Add(dt.ToShortDateString());
        //            iDataValues.Add(Convert.ToString(sdr["NoOfReg"]));
        //        }
        //        model.labels = iLables;
        //        model.dataValues = iDataValues;

        //        return model;
        //    }

        //public void GetDatatable()
        //{
        //    List<KHDataViewDetailModel> lstProperty = new List<KHDataViewDetailModel>();

        //    using (SqlConnection con = new SqlConnection(_ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("sp_GetDatapages", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@PropertyTypeId", model.PropertyTypeId);
        //        cmd.Parameters.AddWithValue("@StatusId", model.StatusId);
        //        cmd.Parameters.AddWithValue("@CityId", model.CityId);
        //        cmd.Parameters.AddWithValue("@AreaId", model.AreaId);
        //        cmd.Parameters.AddWithValue("@PropertyValueMin", model.PropertyValueMin);
        //        cmd.Parameters.AddWithValue("@PropertyValueMax", model.PropertyValueMax);
        //        cmd.Parameters.AddWithValue("@PropertySizeMin", model.PropertySizeMin);
        //        cmd.Parameters.AddWithValue("@PropertySizeMax", model.PropertySizeMax);
        //        cmd.Parameters.AddWithValue("@BedroomMin", model.BedroomsMin);
        //        cmd.Parameters.AddWithValue("@BedroomMax", model.BedroomsMax);
        //        cmd.Parameters.AddWithValue("@ViewsMin", model.PropertyViewsMin);
        //        cmd.Parameters.AddWithValue("@ViewsMax", model.PropertyViewsMax);
        //        cmd.Parameters.AddWithValue("@LikesMin", model.PropertyLikesMin);
        //        cmd.Parameters.AddWithValue("@LikesMax", model.PropertyLikesMax);
        //        cmd.Parameters.AddWithValue("@SelectedOption", string.IsNullOrEmpty(model.SelectedOptions) ? "" : model.SelectedOptions);
        //        cmd.Parameters.AddWithValue("@AgentId", model.AgentId);
        //        if (model.FromDate == model.ToDate)
        //        {
        //            cmd.Parameters.AddWithValue("@FromDate", Convert.ToDateTime("2020-01-01"));
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@FromDate", model.FromDate);
        //        }
        //        cmd.Parameters.AddWithValue("@ToDate", model.ToDate);


        //        con.Open();
        //        SqlDataReader sdr = cmd.ExecuteReader();

        //        while (sdr.Read())
        //        {
        //            KHDataViewDetailModel property = new KHDataViewDetailModel();

        //            property.PropertyId = Convert.ToInt32(sdr["Id"]);
        //            property.Title = Convert.ToString(sdr["Title"]);
        //            property.AgentId = Convert.ToInt32(sdr["AgentId"]);
        //            property.AgentName = Convert.ToString(sdr["AgentName"]);
        //            property.AgentContact = Convert.ToString(sdr["AgentContact"]);
        //            property.Price = Convert.ToDouble(sdr["Price"]);
        //            property.CityId = Convert.ToInt32(sdr["CityId"]);
        //            property.AreaId = Convert.ToInt32(sdr["AreaId"]);
        //            property.CityName = Convert.ToString(sdr["CityName"]);
        //            property.AreaName = Convert.ToString(sdr["AreaName"]);
        //            property.Size = Convert.ToDouble(sdr["Size"]);
        //            property.Bedrooms = Convert.ToInt32(sdr["BathRoomCount"]);
        //            property.Views = Convert.ToInt32(sdr["TotalView"]);
        //            property.Likes = Convert.ToInt32(sdr["Likes"]);



        //            lstProperty.Add(property);
        //        }
        //        model.DetailData = lstProperty;
        //        con.Close();
        //    }
        //    return model;
        //}

    }
}
//}
