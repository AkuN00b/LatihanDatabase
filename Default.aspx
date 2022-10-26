<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LatihanDatabase.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manajemen Data Karyawan</title>
    <link rel="stylesheet" href="Styles/Style.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.9.1/font/bootstrap-icons.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Data Karyawan</h2>
            <hr />

            <asp:Panel runat="server" ID="panelViewData">
                <asp:LinkButton runat="server" ID="linkTambahBaru" OnClick="linkTambahBaru_Click">+ Tambah Baru</asp:LinkButton>
                <br />

                <label>Pencarian </label>
                <asp:TextBox runat="server" ID="txtPencarian"></asp:TextBox>
                <asp:Button runat="server" ID="btnPencarian" Text="Cari" OnClick="btnPencarian_Click" />
                <br />

                <asp:GridView runat="server" ID="gridData" PageSize="5" AllowPaging="true"
                    AllowSorting="false" DataKeyNames="id" ShowHeader="true"
                    ShowHeaderWhenEmpty="true" EmptyDataText="Tidak ada data"
                    AutoGenerateColumns="false" OnPageIndexChanging="gridData_PageIndexChanging" OnRowCommand="gridData_RowCommand">

                    <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" LastPageText=">>" />

                    <Columns>
                        <asp:BoundField DataField="rownum" HeaderText="No" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="npk" HeaderText="NPK" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="nama" HeaderText="Nama" />
                        <asp:BoundField DataField="provinsi" HeaderText="Provinsi" />
                        <asp:TemplateField HeaderText="Aksi" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="linkUbah" CommandName="Ubah" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="bi bi-pencil-square"></i></asp:LinkButton>&nbsp;
                                <asp:LinkButton runat="server" ID="linkHapus" CommandName="Hapus" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="bi bi-trash"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>

            <asp:Panel runat="server" ID="panelManupulasiData" Visible="false">
                <h2>
                    <asp:Literal runat="server" ID="literalTitle"></asp:Literal>
                </h2>
                <asp:TextBox runat="server" ID="hiddenID" Visible="false"></asp:TextBox>

                <label>NPK</label>
                <br />
                <asp:TextBox runat="server" ID="txtNPK"></asp:TextBox>
                <br />
                <br />

                <label>Nama</label>
                <br />
                <asp:TextBox runat="server" ID="txtNama"></asp:TextBox>
                <br />
                <br />

                <label>Provinsi</label>
                <br />
                <asp:DropDownList runat="server" ID="ddProvinsi"></asp:DropDownList>
                <br />
                <br />

                <asp:Button runat="server" ID="btnKirim" Text="Simpan" OnClick="btnKirim_Click" />
            </asp:Panel>
        </div>
    </form>
</body>
</html>
