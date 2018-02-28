﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace MySchoolSolution
{
    public partial class Fee_Structure : Form
    {
        public string NewClass { get; set; }
        public Fee_Structure()
        {
            InitializeComponent();
            txtSession.Text = CommonFunctions.GetCurrentSession;
            ddlClass.DataSource = CommonFunctions.GetClasses;
            lblSession.Text = CommonFunctions.GetCurrentSession;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if(CommonFunctions.GetCurrentSession>)
            try
            {
                if (GetClassForNewEntry() == true)
                {
                    MessageBox.Show("The Fee structure is already saved for this class and session !");
                }
                else
                {

                    SqlParameter[] m = new SqlParameter[22];
                    m[0] = new SqlParameter("@FeeId", SqlDbType.Int);
                    m[1] = new SqlParameter("@SessionYear", txtSession.Text);
                    m[2] = new SqlParameter("@Class", ddlClass.SelectedItem);
                    m[3] = new SqlParameter("@RegistrationFee", txtRegistrationFee.Text);
                    m[4] = new SqlParameter("@AdmissionFee", txtAdmissionFee.Text);
                    m[5] = new SqlParameter("@AnnualCharge", txtAnnualDevFee.Text);
                    m[6] = new SqlParameter("@TutionFee", txtTutionFee.Text);
                    m[7] = new SqlParameter("@ChildFund", txtChildFund.Text);
                    m[8] = new SqlParameter("@Misc", txtMisc.Text);
                    m[9] = new SqlParameter("@ComputerFee", txtComputerFee.Text);
                    m[10] = new SqlParameter("@ExamFee", txtExamFee.Text);
                    m[11] = new SqlParameter("@LateFee", txtLateFee.Text);
                    m[12] = new SqlParameter("@SecurityDeposite", txtSecurityDeposite.Text);
                    m[13] = new SqlParameter("@TransportFee", txtAssignmentFee.Text);
                    m[14] = new SqlParameter("@ScienceFee", txtScienceFee.Text);
                    m[15] = new SqlParameter("@SportsFee", txtSportsFee.Text);
                    m[16] = new SqlParameter("@LibraryFee", txtLibraryFee.Text);
                    m[17] = new SqlParameter("@IsActive", SqlDbType.Text);
                    m[18] = new SqlParameter("@ReportCardFee", txtReportCardFee.Text);
                    m[19] = new SqlParameter("@UDF1", SqlDbType.Text);
                    m[20] = new SqlParameter("@UDF2", SqlDbType.Text);
                    m[21] = new SqlParameter("@UDF3", SqlDbType.Text);
                    GetClassForNewEntry();
                    SqlHelper.ExecuteNonQuery(Connection.Connection_string, CommandType.StoredProcedure, "ClasswiseAnnualFeeStructure_Insert", m);
                    MessageBox.Show("Fee Structure Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Fee_Structure_Load(object sender, EventArgs e)
        {
            //GetClassForNewEntry();
            //ddlClass.SelectedItem = NewClass.ToString();
        }
        private bool GetClassForNewEntry()
        {
            SqlParameter[] m = new SqlParameter[2];
            m[0] = new SqlParameter("@Session", lblSession.Text);
            m[1] = new SqlParameter("@Class", ddlClass.Text);

            DataSet ds = SqlHelper.ExecuteDataset(Connection.Connection_string, "ClasswiseAnnualFeeStructure_SelectAllByClassAndSession", m);
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;

        }

        private void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}