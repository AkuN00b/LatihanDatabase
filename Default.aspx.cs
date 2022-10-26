using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Web.UI.WebControls;

namespace LatihanDatabase
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData(txtPencarian.Text);
                getListProvinsi();
            }
            gridData.Width = Unit.Percentage(100);
        }

        protected void loadData(string query)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                conn.Open();

                //SqlCommand command = new SqlCommand("select ROW_NUMBER() over (order by id asc) as rownum, * from karyawan where nama like '%" + query + "%';", conn);
                //dt.Load(command.ExecuteReader());

                //SqlDataAdapter adapter = new SqlDataAdapter();
                //adapter.SelectCommand = new SqlCommand();
                //adapter.SelectCommand.Connection = conn;
                //adapter.SelectCommand.Parameters.AddWithValue("@query", query);
                //adapter.SelectCommand.CommandText = "sp_getDataKaryawan";
                //adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                //adapter.Fill(dt);

                SqlCommand command = new SqlCommand("sp_getDataKaryawan", conn);
                command.Parameters.AddWithValue("@query", query);
                command.CommandType = CommandType.StoredProcedure;
                dt.Load(command.ExecuteReader());

                gridData.DataSource = dt;
                gridData.DataBind();

                conn.Close();
            }
            catch { }
        }

        protected void getListProvinsi()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                conn.Open();

                // SqlCommand command = new SqlCommand("select id, nama from provinsi order by nama asc;", conn);
                // dt.Load(command.ExecuteReader());

                SqlCommand command = new SqlCommand("sp_getListProvinsi", conn);
                command.CommandType = CommandType.StoredProcedure;
                dt.Load(command.ExecuteReader());

                ddProvinsi.Items.Clear();
                ddProvinsi.DataSource = dt;

                ddProvinsi.DataValueField = "id";
                ddProvinsi.DataTextField = "nama";
                ddProvinsi.DataBind();
                ddProvinsi.Items.Insert(0, new ListItem("-- Pilih Provinsi --", ""));

                conn.Close();
            }
            catch { }
        }

        protected void btnPencarian_Click(object sender, EventArgs e)
        {
            loadData(txtPencarian.Text);
        }

        protected void linkTambahBaru_Click(object sender, EventArgs e)
        {
            clearForm();
            panelViewData.Visible = false;
            panelManupulasiData.Visible = true;
            literalTitle.Text = "Form Tambah Data Karyawan";
        }

        protected void btnKirim_Click(object sender, EventArgs e)
        {
            if (hiddenID.Text.Equals(""))
                createDataKaryawan();
            else
                updateDataKaryawan(hiddenID.Text);

            panelViewData.Visible = true;
            panelManupulasiData.Visible = false;
        }

        protected void createDataKaryawan()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                conn.Open();

                //SqlCommand command =
                //    new SqlCommand("insert into karyawan (npk, nama, provinsi)" +
                //    "values ('" + txtNPK.Text + "', '" + txtNama.Text + "', '" + ddProvinsi.SelectedItem.Text + "');", conn);

                //SqlDataAdapter adapter = new SqlDataAdapter();

                //adapter.InsertCommand = command;
                //adapter.InsertCommand.ExecuteNonQuery();

                SqlCommand command = new SqlCommand("sp_createDataKaryawan", conn);
                command.Parameters.AddWithValue("@npk", txtNPK.Text);
                command.Parameters.AddWithValue("@nama", txtNama.Text);
                command.Parameters.AddWithValue("@provinsi", ddProvinsi.SelectedItem.Text);
                command.CommandType = CommandType.StoredProcedure;
                dt.Load(command.ExecuteReader());

                conn.Close();

                loadData(txtPencarian.Text);
            }
            catch { }
        }

        protected void deleteDataKaryawan(string id)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                conn.Open();

                //SqlCommand command =
                //    new SqlCommand("delete from karyawan where id = " + id + ";", conn);

                //SqlDataAdapter adapter = new SqlDataAdapter();

                //adapter.DeleteCommand = command;
                //adapter.DeleteCommand.ExecuteNonQuery();

                SqlCommand command = new SqlCommand("sp_deleteDataKaryawan", conn);
                command.Parameters.AddWithValue("@id", id);
                command.CommandType = CommandType.StoredProcedure;
                dt.Load(command.ExecuteReader());

                conn.Close();

                loadData(txtPencarian.Text);
            }
            catch { }
        }

        protected void updateDataKaryawan(string id)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                conn.Open();

                //SqlCommand command =
                //    new SqlCommand("update karyawan set npk = '" + txtNPK.Text + "', nama = '" + txtNama.Text + "', provinsi = '" + ddProvinsi.SelectedItem.Text + "' where id = " + id + ";", conn);

                //SqlDataAdapter adapter = new SqlDataAdapter();

                //adapter.UpdateCommand = command;
                //adapter.UpdateCommand.ExecuteNonQuery();

                SqlCommand command = new SqlCommand("sp_updateDataKaryawan", conn);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@npk", txtNPK.Text);
                command.Parameters.AddWithValue("@nama", txtNama.Text);
                command.Parameters.AddWithValue("@provinsi", ddProvinsi.SelectedItem.Text);
                command.CommandType = CommandType.StoredProcedure;
                dt.Load(command.ExecuteReader());

                conn.Close();

                loadData(txtPencarian.Text);
            }
            catch { }
        }

        protected void clearForm()
        {
            hiddenID.Text = "";
            txtNPK.Text = "";
            txtNama.Text = "";
            ddProvinsi.SelectedIndex = 0;
            txtPencarian.Text = "";
        }

        protected void gridData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridData.PageIndex = e.NewPageIndex;
            loadData(txtPencarian.Text);
        }

        protected void gridData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {
                string tempId = gridData.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();

                if (e.CommandName == "Hapus")
                {
                    deleteDataKaryawan(tempId);
                }
                else if (e.CommandName == "Ubah")
                {
                    hiddenID.Text = tempId;
                    panelViewData.Visible = false;
                    panelManupulasiData.Visible = true;
                    literalTitle.Text = "Form Ubah Data Karyawan";

                    try
                    {
                        DataTable dt = new DataTable();

                        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                        conn.Open();

                        // SqlCommand command = new SqlCommand("select npk, nama, provinsi from karyawan where id = '" + tempId + "';", conn);

                        SqlCommand command = new SqlCommand("sp_getDataForUpdateKaryawan", conn);
                        command.Parameters.AddWithValue("@id", tempId);
                        command.CommandType = CommandType.StoredProcedure;

                        dt.Load(command.ExecuteReader());

                        txtNPK.Text = dt.Rows[0][0].ToString();
                        txtNama.Text = dt.Rows[0][1].ToString();
                        ddProvinsi.SelectedItem.Text = dt.Rows[0][2].ToString();

                        conn.Close();
                    }
                    catch { }
                }
            }
        }
    }
}