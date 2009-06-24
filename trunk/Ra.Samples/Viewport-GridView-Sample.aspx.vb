'
' Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
' Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
' This code is licensed under the GPL version 3 which 
' can be found in the license.txt file on disc.
' 


Imports System
Imports Ra.Widgets
Imports Ra.Extensions
Imports Ra.Extensions.Widgets
Imports Ra.Effects

Namespace Samples
    Partial Public Class ViewportGridView
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            ' Remove this line, just here for "testing purposes" to make it easy to "log out"
            If Not IsPostBack Then
                PeopleDatabase.Filter = ""
                PeopleDatabase.Sorting = PeopleDatabase.Sort.Name
                DataBindGridView()
            End If
        End Sub

        Private Property PageIndex() As Integer
            Get
                If ViewState("GridPage") = Nothing Then
                    Return 0
                End If
                Return CType(ViewState("GridPage"), Integer)
            End Get
            Set(ByVal value As Integer)
                ViewState("GridPage") = value
            End Set
        End Property

        Private Property PageSize() As Integer
            Get
                If ViewState("PageSize") = Nothing Then
                    Return 5
                End If
                Return CType(ViewState("PageSize"), Integer)
            End Get
            Set(ByVal value As Integer)
                ViewState("PageSize") = value
            End Set
        End Property

        Private Sub DataBindGridView()
            Dim src As New System.Web.UI.WebControls.PagedDataSource()
            src.DataSource = PeopleDatabase.Database
            src.AllowPaging = True
            src.PageSize = PageSize
            src.CurrentPageIndex = PageIndex
            grid.DataSource = src
            grid.DataBind()
            Dim pages As Integer = PeopleDatabase.Database.Count \ PageSize + IIf(PeopleDatabase.Database.Count Mod PageSize > 0, 1, 0)
            wndRight.Caption = "Main Content - " & PeopleDatabase.Database.Count _
                & " Records - Showing page " & (PageIndex + 1).ToString() _
                & " of " & pages
        End Sub

        Protected Sub filter_KeyUp(ByVal sender As Object, ByVal e As EventArgs)
            PeopleDatabase.Filter = filter.Text
            PageIndex = 0
            DataBindGridView()
            pnlRight.ReRender()
        End Sub

        Protected Sub EscClicked(ByVal sender As Object, ByVal e As EventArgs)
            editWindow.Visible = False
        End Sub

        Protected Sub SortName(ByVal sender As Object, ByVal e As EventArgs)
            PeopleDatabase.Sorting = PeopleDatabase.Sort.Name
            PageIndex = 0
            filter.Text = ""
            PeopleDatabase.Filter = ""
            DataBindGridView()
            pnlRight.ReRender()
        End Sub

        Protected Sub SortAddress(ByVal sender As Object, ByVal e As EventArgs)
            PeopleDatabase.Sorting = PeopleDatabase.Sort.Adr
            PageIndex = 0
            filter.Text = ""
            PeopleDatabase.Filter = ""
            DataBindGridView()
            pnlRight.ReRender()
        End Sub

        Protected Sub SortBirthday(ByVal sender As Object, ByVal e As EventArgs)
            PeopleDatabase.Sorting = PeopleDatabase.Sort.Birthdate
            PageIndex = 0
            filter.Text = ""
            PeopleDatabase.Filter = ""
            DataBindGridView()
            pnlRight.ReRender()
        End Sub

        Protected Sub EditEntry(ByVal sender As Object, ByVal e As EventArgs)
            editWindow.Visible = True
            editBirth.Visible = False
            editShowCalendar.Visible = True
            editName.Select()
            editName.Focus()
            Dim hid As HiddenField = CType(CType(sender, RaControl).Parent.Controls(1), HiddenField)
            Dim g As New Guid(hid.Value)
            Dim p As People = Nothing
            For Each idx As People In PeopleDatabase.Database
                If idx.ID = g Then
                    p = idx
                End If
            Next
            editName.Text = p.Name
            editWindow.Caption = "Edit; " & p.Name
            editAdr.Text = p.Address
            editHidden.Value = p.ID.ToString()
            editShowCalendar.Text = p.Birthday.ToString("dddd dd.MMMM yyyy", System.Globalization.CultureInfo.InvariantCulture)
            editBirth.Value = p.Birthday
        End Sub

        Protected Sub editShowCalendar_Click(ByVal sender As Object, ByVal e As EventArgs)
            editBirth.Visible = True
            editShowCalendar.Visible = False
            Dim effect As New EffectRollDown(editBirth, 200)
            effect.Render()
        End Sub

        Protected Sub editSave_Click(ByVal sender As Object, ByVal e As EventArgs)
            editWindow.Visible = False

            Dim g As New Guid(editHidden.Value)
            Dim p As People = Nothing
            For Each idx As People In PeopleDatabase.Database
                If idx.ID = g Then
                    p = idx
                End If
            Next
            p.Name = editName.Text
            p.Address = editAdr.Text
            p.Birthday = editBirth.Value
            DataBindGridView()
            pnlRight.ReRender()

            Dim effect As New EffectTimeout(500)
            effect.ChainThese(New EffectHighlight(pnlRightOuter, 500))
            effect.Render()
        End Sub

        Protected Sub editBirth_DateClicked(ByVal sender As Object, ByVal e As EventArgs)
            editBirth.Visible = False
            editShowCalendar.Visible = True
            editShowCalendar.Text = editBirth.Value.ToString("dddd dd.MMMM yyyy", System.Globalization.CultureInfo.InvariantCulture)
        End Sub

        Protected Sub wndRight_CreateTitleBarControls(ByVal sender As Object, ByVal e As Window.CreateTitleBarControlsEventArgs)
            Dim prev As New LinkButton()
            prev.ID = "prev"
            prev.CssClass = "window_prev"
            prev.AccessKey = "P"
            prev.Tooltip = "Click to page to previous (ALT+SHIFT+P in FireFox)"
            AddHandler prev.Click, AddressOf prev_Click
            e.Caption.Controls.Add(prev)

            Dim nxt As New LinkButton()
            nxt.ID = "next"
            nxt.CssClass = "window_next"
            nxt.AccessKey = "N"
            nxt.Tooltip = "Click to page to next  (ALT+SHIFT+N in FireFox)"
            AddHandler nxt.Click, AddressOf nxt_Click
            e.Caption.Controls.Add(nxt)
        End Sub

        Protected Sub prev_Click(ByVal sender As Object, ByVal e As EventArgs)
            If PageIndex = 0 Then
                Return
            End If
            PageIndex -= 1
            DataBindGridView()
            pnlRight.ReRender()
            Dim eff As New EffectRollUp(pnlRight, 200)
            eff.ChainThese(New EffectRollDown(pnlRight, 200))
            eff.Render()
        End Sub

        Protected Sub nxt_Click(ByVal sender As Object, ByVal e As EventArgs)
            If (PageIndex + 1) * PageSize > PeopleDatabase.Database.Count Then
                Return
            End If
            PageIndex += 1
            DataBindGridView()
            pnlRight.ReRender()
            Dim eff As New EffectRollUp(pnlRight, 200)
            eff.ChainThese(New EffectRollDown(pnlRight, 200))
            eff.Render()
        End Sub

        Protected Sub resizer_Resized(ByVal sender As Object, ByVal e As ResizeHandler.ResizedEventArgs)
            Dim width As Integer = Math.Max(e.Width - 264, 400)
            Dim height As Integer = Math.Max(e.Height - 101, 200)
            Dim heightLeft As Integer = Math.Max(e.Height - 390, 50)
            wndRight.Style("width") = width.ToString() & "px"
            pnlRightOuter.Style("height") = height.ToString() & "px"
            pnlLeft.Style("height") = heightLeft.ToString() & "px"
        End Sub
    End Class
End Namespace
