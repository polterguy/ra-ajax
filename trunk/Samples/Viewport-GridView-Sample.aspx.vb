'
' Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
' Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
' This code is licensed under the LGPL version 3 which 
' can be found in the license.txt file on disc.
' 


Imports System
Imports Ra.Widgets
Imports Ra.Extensions

Namespace Samples
    Public Partial Class ViewportGridView
        Inherits System.Web.UI.Page
        Private _triedToLogin As Boolean
        
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            ' Remove this line, just here for "testing purposes" to make it easy to "log out"
            If Not IsPostBack Then
                Session("user") = Nothing
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
            Dim src As New PagedDataSource()
            src.DataSource = PeopleDatabase.Database
            src.AllowPaging = True
            src.PageSize = PageSize
            src.CurrentPageIndex = PageIndex
            grid.DataSource = src
            grid.DataBind()
            wndRight.Caption = "Main Content - " & (PageIndex + 1).ToString() & "/" & CType((PeopleDatabase.Database.Count / PageSize).ToString(), Integer)
        End Sub

        Protected Sub EditEntry(ByVal sender As Object, ByVal e As EventArgs)
            editWindow.Visible = True
            Dim hid As HiddenField = CType(CType(sender, RaControl).Parent.Controls(1), HiddenField)
            Dim g As New Guid(hid.Value)
            Dim p As People = Nothing
            For Each idx As People In PeopleDatabase.Database
                If idx.ID = g Then
                    p = idx
                End If
            Next
            editName.Text = p.Name
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
            effect.ChainThese(New EffectHighlight(pnlRight, 500))
            effect.Render()
        End Sub

        Protected Sub editBirth_DateClicked(ByVal sender As Object, ByVal e As EventArgs)
            editBirth.Visible = False
            editShowCalendar.Visible = True
            editShowCalendar.Text = editBirth.Value.ToString("dddd dd.MMMM yyyy", System.Globalization.CultureInfo.InvariantCulture)
        End Sub

        ' We want to wait as long as possible with this logic to make sure we're NOT
        ' running this logic e.e. when user is actually logging in etc...
        Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
            If Session("user") Is Nothing AndAlso Not _triedToLogin AndAlso Not loginWnd.Visible Then
                ' Uncomment the line below to have "true" login logic...
                ' This line is commented just to make everything show for "bling reasons"...
                'everything.Visible = false;
                loginWnd.Visible = True
                Dim effect As Effect = New EffectFadeIn(loginWnd, 1000)
                effect.Render()
                username.Text = "username"
                password.Text = "password"
                username.Select()
                username.Focus()
            End If
            MyBase.OnPreRender(e)
        End Sub

        Protected Sub wndRight_CreateNavigationalButtons(ByVal sender As Object, ByVal e As Window.CreateNavigationalButtonsEvtArgs)
            Dim prev As New LinkButton()
            prev.ID = "prev"
            prev.CssClass = "window_prev"
            prev.ToolTip = "Click to page to previous"
            AddHandler prev.Click, AddressOf prev_Click
            e.Caption.Controls.Add(prev)

            Dim nxt As New LinkButton()
            nxt.ID = "next"
            nxt.CssClass = "window_next"
            nxt.ToolTip = "Click to page to next"
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
        End Sub

        Protected Sub nxt_Click(ByVal sender As Object, ByVal e As EventArgs)
            If (PageIndex + 1) * PageSize > PeopleDatabase.Database.Count Then
                Return
            End If
            PageIndex += 1
            DataBindGridView()
            pnlRight.ReRender()
        End Sub

        Protected Sub resizer_Resized(ByVal sender As Object, ByVal e As ResizeHandler.ResizedEventArgs)
            lbl.Text = String.Format("Width: {0}, Height: {1}", e.Width, e.Height)
            Dim effect = New EffectHighlight(lbl, 500)
            effect.Render()
            Dim width As Integer = Math.Max(e.Width - 264, 400)
            Dim height As Integer = Math.Max(e.Height - 101, 200)
            Dim heightLeft As Integer = Math.Max(e.Height - 390, 50)
            wndRight.Style("width") = width.ToString() & "px"
            pnlRight.Style("height") = height.ToString() & "px"
            pnlLeft.Style("height") = heightLeft.ToString() & "px"
        End Sub

        Protected Sub loginBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
            _triedToLogin = True
            If username.Text = "admin" And password.Text = "admin" Then
                loginWnd.Visible = False
                everything.Visible = True
                Session("user") = "admin"
            Else
                loginInfo.Text = "Username; <strong>'admin'</strong>, Password; <strong>'admin'</strong>"
                Dim effect = New EffectHighlight(loginInfo, 500)
                effect.Render()
                username.Select()
                username.Focus()
            End If
        End Sub
    End Class
End Namespace
