Imports System.Drawing

Public Class ReloadImage

#Region "Private Variable"
    'Private Variable
    Private imgNewSales As Bitmap
    Private imgCancel As Bitmap
    Private imgBrowse As Bitmap
    Private imgPayment As Bitmap
    Private imgClosing As Bitmap
    Private imgSave As Bitmap
    Private imgEdit As Bitmap
    Private imgPromo As Bitmap
    Private imgVoid As Bitmap
    Private imgPrint As Bitmap
    Private imgValidate As Bitmap
    Private imgPosting As Bitmap

    Private imgRefresh As Bitmap
    Private imgCash As Bitmap
    Private imgCard As Bitmap
    Private imgVoucher As Bitmap
    Private imgSetupPrinter As Bitmap

    Private imgLogoCompany As Bitmap
    Private imgMyLogo As Bitmap
  
    Private imgReport As Bitmap
    Private imgExport As Bitmap

    'Menu Title
    Private imgSearch As Bitmap
    Private imgTagProduct As Bitmap
    Private imgShopingCart As Bitmap
    Private imgInterfacing As Bitmap
    Private imgParameter As Bitmap
    Private imgUsers As Bitmap
    Private imgReporting As Bitmap
    Private imgPurchase As Bitmap
    Private imgReceive As Bitmap

    Private imgUserGroup As Bitmap
    Private imgUserLogin As Bitmap

    Private imgEdit2 As Bitmap
    Private imgSearch2 As Bitmap
#End Region

#Region "Constructor"
    'Constructor
    Public Sub New()
        imgNewSales = New Bitmap(My.Resources.Add)
        imgCancel = New Bitmap(My.Resources.Cancel)
        imgBrowse = New Bitmap(My.Resources.Browse)
        imgPayment = New Bitmap(My.Resources.Payment)
        imgPromo = New Bitmap(My.Resources.Promo)
        imgSave = New Bitmap(My.Resources.Save)
        imgEdit = New Bitmap(My.Resources.Edit)
        imgPromo = New Bitmap(My.Resources.EventPromo)
        imgVoid = New Bitmap(My.Resources.Void)
        imgPrint = New Bitmap(My.Resources.Print)
        imgValidate = New Bitmap(My.Resources.Apply)
        imgPosting = New Bitmap(My.Resources.Posting)

        imgEdit2 = New Bitmap(My.Resources.edit2)
        imgSearch2 = New Bitmap(My.Resources.search2)

        imgClosing = New Bitmap(My.Resources.Close)
        imgRefresh = New Bitmap(My.Resources.Refresh)
        imgCash = New Bitmap(My.Resources.Cash)
        imgCard = New Bitmap(My.Resources.Card)
        imgExport = New Bitmap(My.Resources.Export)
        imgSetupPrinter = New Bitmap(My.Resources.SetupPrinter)

        imgLogoCompany = New Bitmap(My.Resources.logo_company)
        imgMyLogo = New Bitmap(My.Resources.my_logo)


        'Label
        imgSearch = New Bitmap(My.Resources.Search)
        imgTagProduct = New Bitmap(My.Resources.Tag)
        imgShopingCart = New Bitmap(My.Resources.Shopping_Cart)
        imgInterfacing = New Bitmap(My.Resources.Change)
        imgParameter = New Bitmap(My.Resources.Parameter)
        imgUsers = New Bitmap(My.Resources.Users)
        imgReporting = New Bitmap(My.Resources.Report)
        imgPurchase = New Bitmap(My.Resources.Delivery)
        imgReceive = New Bitmap(My.Resources.Receive)

        imgUserGroup = New Bitmap(My.Resources.UserGroup)
        imgUserLogin = New Bitmap(My.Resources.UserLogin)

    End Sub

#End Region

#Region "Property"


    'Property
    Public ReadOnly Property ImgBtnValidate() As Bitmap
        Get
            Return imgValidate
        End Get

    End Property

    Public ReadOnly Property ImgBtnPosting() As Bitmap
        Get
            Return imgPosting
        End Get

    End Property

    Public ReadOnly Property ImgBtnVoucher() As Bitmap
        Get
            Return imgVoucher
        End Get

    End Property

    Public ReadOnly Property ImgBtnEdit2S() As Bitmap
        Get
            Return imgEdit2
        End Get

    End Property


    Public ReadOnly Property ImgBtnSearch2S() As Bitmap
        Get
            Return imgSearch2
        End Get

    End Property


    Public ReadOnly Property ImgBtnSetupPrinter() As Bitmap
        Get
            Return imgSetupPrinter
        End Get

    End Property

    Public ReadOnly Property ImgPicPromo() As Bitmap
        Get
            Return imgPromo
        End Get

    End Property

    Public ReadOnly Property ImgBtnExport() As Bitmap
        Get
            Return imgExport
        End Get

    End Property

    Public ReadOnly Property ImgBtnCash() As Bitmap
        Get
            Return imgCash
        End Get

    End Property

    Public ReadOnly Property ImgBtnCard() As Bitmap
        Get
            Return imgCard
        End Get

    End Property

    Public ReadOnly Property LogoCompany() As Bitmap
        Get
            Return imgLogoCompany
        End Get

    End Property

    Public ReadOnly Property MyLogo() As Bitmap
        Get
            Return imgMyLogo
        End Get

    End Property

    Public ReadOnly Property ImgBtnNew() As Bitmap
        Get
            Return imgNewSales
        End Get

    End Property


    Public ReadOnly Property ImgBtnCancel() As Bitmap
        Get
            Return imgCancel
        End Get

    End Property

    Public ReadOnly Property ImgBtnVoid() As Bitmap
        Get
            Return imgVoid
        End Get

    End Property

    Public ReadOnly Property ImgBtnPrint() As Bitmap
        Get
            Return imgPrint
        End Get

    End Property

    Public ReadOnly Property ImgBtnBrowse() As Bitmap
        Get
            Return imgBrowse
        End Get

    End Property

    Public ReadOnly Property ImgBtnPayment() As Bitmap
        Get
            Return imgPayment
        End Get

    End Property

    Public ReadOnly Property ImgBtnPromo() As Bitmap
        Get
            Return imgPromo
        End Get

    End Property

    Public ReadOnly Property ImgBtnSave() As Bitmap
        Get
            Return imgSave
        End Get

    End Property

    Public ReadOnly Property ImgBtnEdit() As Bitmap
        Get
            Return imgEdit
        End Get

    End Property

    Public ReadOnly Property ImgBtnClosing() As Bitmap
        Get
            Return imgClosing
        End Get

    End Property

    Public ReadOnly Property ImgBtnRefresh() As Bitmap
        Get
            Return imgRefresh
        End Get

    End Property

   

    Public ReadOnly Property ImgMnuReport() As Bitmap
        Get
            Return imgReport
        End Get

    End Property

    'Label
    Public ReadOnly Property ImgLabelSearch() As Bitmap
        Get
            Return imgSearch
        End Get

    End Property

    Public ReadOnly Property ImgLabelTag() As Bitmap
        Get
            Return imgTagProduct
        End Get

    End Property

    Public ReadOnly Property ImgLabelShoppingCart() As Bitmap
        Get
            Return imgShopingCart
        End Get

    End Property

    Public ReadOnly Property ImgLabelInterfacing() As Bitmap
        Get
            Return imgInterfacing
        End Get

    End Property

    Public ReadOnly Property ImgLabelParameter() As Bitmap
        Get
            Return imgParameter
        End Get

    End Property


    Public ReadOnly Property ImgLabelUsers() As Bitmap
        Get
            Return imgUsers
        End Get

    End Property


    Public ReadOnly Property ImgLabelReporting() As Bitmap
        Get
            Return imgReporting
        End Get

    End Property


    Public ReadOnly Property ImgLabelPurchase() As Bitmap
        Get
            Return imgPurchase
        End Get

    End Property


    Public ReadOnly Property ImgLabelReceive() As Bitmap
        Get
            Return imgReceive
        End Get

    End Property

    Public ReadOnly Property ImgLoginUserGroup() As Bitmap
        Get
            Return imgUserGroup
        End Get

    End Property


    Public ReadOnly Property ImgLoginUser() As Bitmap
        Get
            Return imgUserLogin
        End Get

    End Property

#End Region

End Class
