Imports proLib.Process
Imports genLib.General
Imports sqlLib.Sql

Module CreateSubMenu

    Private frm As Form

    Public Sub SubMenuSales(ByVal mnuMain As MenuStrip)
        Dim mnuStoresSales As ToolStripMenuItem
        Dim mnuCompanySales As ToolStripMenuItem
       
        'clear menustrip items
        mnuMain.Items.Clear()

        mnuStoresSales = New ToolStripMenuItem
        With mnuStoresSales
            .Text = "Stores"
            .Name = "mnuStores"
            .Tag = "SSTO"
        End With

        mnuCompanySales = New ToolStripMenuItem
        With mnuCompanySales
            .Text = "Company"
            .Name = "mnuCompany"
            .Tag = "SCOMP"
        End With

        For i As Integer = 0 To tblMenuAccess.Rows.Count - 1

        Next
        mnuMain.Items.Add(mnuStoresSales)
        mnuMain.Items.Add(mnuCompanySales)

        DropMenuStoresSales(mnuStoresSales)

    End Sub

    Public Sub SubMenuFinance(ByVal mnuMain As MenuStrip)
        Dim mnuStoresFinance As ToolStripMenuItem
        Dim mnuCompanyFinance As ToolStripMenuItem

        'clear menustrip items
        mnuMain.Items.Clear()

        mnuStoresFinance = New ToolStripMenuItem
        With mnuStoresFinance
            .Text = "Stores"
            .Name = "mnuStoresFinance"
        End With

        mnuCompanyFinance = New ToolStripMenuItem
        With mnuCompanyFinance
            .Text = "Company"
            .Name = "mnuCompanyFinance"
        End With

        mnuMain.Items.Add(mnuStoresFinance)
        mnuMain.Items.Add(mnuCompanyFinance)

        DropMenuStoresFinance(mnuStoresFinance)

    End Sub

    Public Sub SubMenuInventory(ByVal mnuMain As MenuStrip)
        Dim mnuStoresInventory As ToolStripMenuItem
        Dim mnuCompanyInventory As ToolStripMenuItem
        Dim mnuApproval As ToolStripMenuItem

        'clear menustrip items
        mnuMain.Items.Clear()

        mnuStoresInventory = New ToolStripMenuItem
        With mnuStoresInventory
            .Text = "Stores"
            .Name = "mnuStoresInventory"
            .Tag = "ISTO"
        End With

        mnuCompanyInventory = New ToolStripMenuItem
        With mnuCompanyInventory
            .Text = "Company"
            .Name = "mnuCompanyInventory"
            .Tag = "ICOMP"
        End With

        mnuApproval = New ToolStripMenuItem
        With mnuApproval
            .Text = "Approval"
            .Name = "mnuApproval"
            .Tag = "IAPRO"

        End With

        mnuMain.Items.Add(mnuStoresInventory)
        mnuMain.Items.Add(mnuCompanyInventory)
        mnuMain.Items.Add(mnuApproval)
        AddHandler mnuApproval.Click, AddressOf MenuItemClicked
        DropMenuStoresInventory(mnuStoresInventory)

    End Sub

    Public Sub SubMenuHRD(ByVal mnuMain As MenuStrip)
        Dim mnuEmployees As ToolStripMenuItem

        'clear menustrip items
        mnuMain.Items.Clear()

        mnuEmployees = New ToolStripMenuItem
        With mnuEmployees
            .Text = "Employees"
            .Name = "mnuEmployees"
        End With
        AddHandler mnuEmployees.Click, AddressOf MenuItemClicked
        mnuMain.Items.Add(mnuEmployees)

    End Sub

    Public Sub SubMenuReport(ByVal mnuMain As MenuStrip)
        Dim mnuSalesReport As ToolStripMenuItem
        Dim mnuInventoryReport As ToolStripMenuItem
        Dim mnuInterfacing As ToolStripMenuItem

        'clear menustrip items
        mnuMain.Items.Clear()

        mnuSalesReport = New ToolStripMenuItem
        With mnuSalesReport
            .Text = "Sales"
            .Name = "mnuSalesReport"
        End With

        mnuInventoryReport = New ToolStripMenuItem
        With mnuInventoryReport
            .Text = "Inventory"
            .Name = "mnuInventoryReport"
        End With

        mnuInterfacing = New ToolStripMenuItem
        With mnuInterfacing
            .Text = "Interfacing"
            .Name = "mnuInterfacing"
        End With

        If Not CanViewMenu(mnuSalesReport.Name) = False Then mnuMain.Items.Add(mnuSalesReport)
        If Not CanViewMenu(mnuInventoryReport.Name) = False Then mnuMain.Items.Add(mnuInventoryReport)
        mnuMain.Items.Add(mnuInterfacing)

        AddHandler mnuInterfacing.Click, AddressOf MenuItemClicked
        DropMenuSalesReport(mnuSalesReport)
        DropMenuInventoryReport(mnuInventoryReport)
    End Sub

    Private Function CanViewMenu(mnuName As String) As Boolean
        For i As Integer = 0 To tblMenuAccess.Rows.Count - 1

            If tblMenuAccess.Rows(i).Item(3) = mnuName And Not tblMenuAccess.Rows(i).Item(4) = 0 Then
                If tblMenuAccess.Rows(i).Item(10) = True Then
                    Return True
                End If
            End If

        Next

        Return False

    End Function

    Public Sub SubMenuProducts(ByVal mnuMain As MenuStrip)
        Dim mnuProducts As ToolStripMenuItem
        Dim mnuDiscounts As ToolStripMenuItem
        Dim mnuPricelists As ToolStripMenuItem
        Dim mnuVouchers As ToolStripMenuItem
        Dim mnuEvent As ToolStripMenuItem
        Dim mnuDisplay As ToolStripMenuItem
        'clear menustrip items
        mnuMain.Items.Clear()

        mnuProducts = New ToolStripMenuItem
        With mnuProducts
            .Text = "Products"
            .Name = "mnuProducts"
            .Tag = "PPRO"
        End With

        mnuDiscounts = New ToolStripMenuItem
        With mnuDiscounts
            .Text = "Discounts"
            .Name = "mnuDiscounts"
            .Tag = "PDISC"
        End With

        mnuPricelists = New ToolStripMenuItem
        With mnuPricelists
            .Text = "Pricelists"
            .Name = "mnuPricelists"
            .Tag = "PPRI"
        End With

        mnuVouchers = New ToolStripMenuItem
        With mnuVouchers
            .Text = "Vouchers"
            .Name = "mnuVouchers"
            .Tag = "PVOU"
        End With

        mnuEvent = New ToolStripMenuItem
        With mnuEvent
            .Text = "EVENT PROMO && BEST PRICE"
            .Name = "mnuEvent"
            .Tag = "PEVE"
        End With

        mnuDisplay = New ToolStripMenuItem
        With mnuDisplay
            .Text = "Display"
            .Name = "mnuDisplay"
            .Tag = "PDISP"
        End With

        mnuMain.Items.Add(mnuProducts)
        'mnuMain.Items.Add(mnuDiscounts)
        'mnuMain.Items.Add(mnuPricelists)
        mnuMain.Items.Add(mnuVouchers)
        mnuMain.Items.Add(mnuEvent)
        mnuMain.Items.Add(mnuDisplay)

        AddHandler mnuProducts.Click, AddressOf MenuItemClicked
        AddHandler mnuVouchers.Click, AddressOf MenuItemClicked
        AddHandler mnuEvent.Click, AddressOf MenuItemClicked
        AddHandler mnuDisplay.Click, AddressOf MenuItemClicked
        DropMenuVouchers(mnuVouchers)
        'AddHandler mnuDiscounts.Click, AddressOf MenuItemClicked
        'AddHandler mnuPricelists.Click, AddressOf MenuItemClicked
    End Sub

    Public Sub SubMenuSetup(ByVal mnuMain As MenuStrip)
        Dim mnuParameter As ToolStripMenuItem
        Dim mnuDatabase As ToolStripMenuItem
        Dim mnuUsers As ToolStripMenuItem
        Dim mnuChangePassword As ToolStripMenuItem
        Dim mnuMenuAccess As ToolStripMenuItem
        Dim mnuEXIM As ToolStripMenuItem

        'clear menustrip items
        mnuMain.Items.Clear()

        mnuParameter = New ToolStripMenuItem
        With mnuParameter
            .Text = "Parameter"
            .Name = "mnuParameter"
        End With

        mnuDatabase = New ToolStripMenuItem
        With mnuDatabase
            .Text = "Database"
            .Name = "mnuDatabase"
        End With

        mnuUsers = New ToolStripMenuItem
        With mnuUsers
            .Text = "Users"
            .Name = "mnuUsers"
        End With

        mnuChangePassword = New ToolStripMenuItem
        With mnuChangePassword
            .Text = "Change Password"
            .Name = "mnuChangePassword"
        End With

        mnuMenuAccess = New ToolStripMenuItem
        With mnuMenuAccess
            .Text = "Menu Access"
            .Name = "mnuMenuAccess"
        End With

        mnuEXIM = New ToolStripMenuItem
        With mnuEXIM
            .Text = "Export/Import"
            .Name = "mnuEXIM"
        End With


        mnuMain.Items.Add(mnuParameter)
        mnuMain.Items.Add(mnuDatabase)
        mnuMain.Items.Add(mnuUsers)
        mnuMain.Items.Add(mnuChangePassword)
        mnuMain.Items.Add(mnuMenuAccess)
        mnuMain.Items.Add(mnuEXIM)


        AddHandler mnuParameter.Click, AddressOf MenuItemClicked
        AddHandler mnuDatabase.Click, AddressOf MenuItemClicked
        AddHandler mnuUsers.Click, AddressOf MenuItemClicked
        AddHandler mnuChangePassword.Click, AddressOf MenuItemClicked
        AddHandler mnuMenuAccess.Click, AddressOf MenuItemClicked
        DropMenuEXIM(mnuEXIM)


    End Sub

    Public Sub SubMenuTools(ByVal mnuMain As MenuStrip)
        Dim mnuGetLastReceive As ToolStripMenuItem
        Dim mnuRealStock As ToolStripMenuItem

        'clear menustrip items
        mnuMain.Items.Clear()

        mnuGetLastReceive = New ToolStripMenuItem
        With mnuGetLastReceive
            .Text = "Get Last Receive"
            .Name = "mnuGetLastReceive"
        End With

        mnuRealStock = New ToolStripMenuItem
        With mnuRealStock
            .Text = "Get Real Stock"
            .Name = "mnuRealStock"
        End With

        mnuMain.Items.Add(mnuGetLastReceive)

        AddHandler mnuGetLastReceive.Click, AddressOf MenuItemClicked

        mnuMain.Items.Add(mnuRealStock)

        AddHandler mnuRealStock.Click, AddressOf MenuItemClicked

    End Sub

    Private Sub DropMenuEXIM(ByVal header As ToolStripMenuItem)
        Dim mnuExportData As ToolStripMenuItem
        Dim mnuImportData As ToolStripMenuItem

        'Cashier Menu
        mnuExportData = New ToolStripMenuItem
        With mnuExportData
            .Text = "Export Data"
            .Name = "mnuExportData"
        End With

        mnuImportData = New ToolStripMenuItem
        With mnuImportData
            .Text = "Import Data"
            .Name = "mnuImportData"

        End With

        AddHandler mnuExportData.Click, AddressOf MenuItemClicked
        AddHandler mnuImportData.Click, AddressOf MenuItemClicked

        header.DropDownItems.Add(mnuExportData)
        header.DropDownItems.Add(mnuImportData)

    End Sub

    Private Sub DropMenuStoresSales(ByVal stores As ToolStripMenuItem)
        Dim mnuOpenCashier As ToolStripMenuItem
        Dim mnuPOS As ToolStripMenuItem
        Dim mnuCloseCashier As ToolStripMenuItem
        Dim mnuSepStoresSales1 As ToolStripSeparator

        Dim mnuSalesOrder As ToolStripMenuItem
        Dim mnuDeliveryOrder As ToolStripMenuItem
        Dim mnuSalesInvoice As ToolStripMenuItem
        Dim mnuSepStoresSales2 As ToolStripSeparator
        Dim mnuExhibition As ToolStripMenuItem

        'Cashier Menu
        mnuOpenCashier = New ToolStripMenuItem
        With mnuOpenCashier
            .Text = "Open Cashier"
            .Name = "mnuOpenCashier"
        End With

        mnuPOS = New ToolStripMenuItem
        With mnuPOS
            .Text = "Point of Sales"
            .Name = "mnuPOS"

        End With

        mnuCloseCashier = New ToolStripMenuItem
        With mnuCloseCashier
            .Text = "Close Cashier"
            .Name = "mnuCloseCashier"
        End With

        mnuSepStoresSales1 = New ToolStripSeparator
        With mnuSepStoresSales1
            .Name = "mnuSepStoresSales1"
        End With

        'Direct Selling Menu
        mnuSalesOrder = New ToolStripMenuItem
        With mnuSalesOrder
            .Text = "Sales Order"
            .Name = "mnuSalesOrder"
        End With

        mnuDeliveryOrder = New ToolStripMenuItem
        With mnuDeliveryOrder
            .Text = "Delivery Order"
            .Name = "mnuDeliveryOrder"
        End With

        mnuSalesInvoice = New ToolStripMenuItem
        With mnuSalesInvoice
            .Text = "Sales Invoice"
            .Name = "mnuSalesInvoice"
        End With

        mnuSepStoresSales2 = New ToolStripSeparator
        With mnuSepStoresSales2
            .Name = "mnuSepStores2"
        End With

        mnuExhibition = New ToolStripMenuItem
        With mnuExhibition
            .Text = "Exhibition"
            .Name = "mnuExhibition"
        End With

        'Cashier
        AddHandler mnuOpenCashier.Click, AddressOf MenuItemClicked
        AddHandler mnuCloseCashier.Click, AddressOf MenuItemClicked
        stores.DropDownItems.Add(mnuOpenCashier)
        AddHandler mnuPOS.Click, AddressOf MenuItemClicked
        stores.DropDownItems.Add(mnuPOS)
        stores.DropDownItems.Add(mnuCloseCashier)
        stores.DropDownItems.Add(mnuSepStoresSales1)

        'Direct Selling
        stores.DropDownItems.Add(mnuSalesOrder)
        stores.DropDownItems.Add(mnuDeliveryOrder)
        stores.DropDownItems.Add(mnuSalesInvoice)
        stores.DropDownItems.Add(mnuSepStoresSales2)

        'Pameran
        AddHandler mnuExhibition.Click, AddressOf MenuItemClicked
        stores.DropDownItems.Add(mnuExhibition)
    End Sub

    Private Sub DropMenuStoresFinance(ByVal stores As ToolStripMenuItem)
        Dim mnuSalesReturn As ToolStripMenuItem
        Dim mnuPosEndofDay As ToolStripMenuItem
        Dim mnuClosingCashier As ToolStripMenuItem

        mnuSalesReturn = New ToolStripMenuItem
        With mnuSalesReturn
            .Text = "Sales Return"
            .Name = "mnuSalesReturn"
        End With

        mnuPosEndofDay = New ToolStripMenuItem
        With mnuPosEndofDay
            .Text = "Pos End of Day"
            .Name = "mnuPosEndofDay"

        End With

        mnuClosingCashier = New ToolStripMenuItem
        With mnuClosingCashier
            .Text = "Closing Cashier"
            .Name = "mnuClosingCashier"

        End With

        AddHandler mnuSalesReturn.Click, AddressOf MenuItemClicked
        AddHandler mnuPosEndofDay.Click, AddressOf MenuItemClicked
        AddHandler mnuClosingCashier.Click, AddressOf MenuItemClicked
        stores.DropDownItems.Add(mnuSalesReturn)
        stores.DropDownItems.Add(mnuPosEndofDay)
        stores.DropDownItems.Add(mnuClosingCashier)
    End Sub

    Private Sub DropMenuStoresInventory(ByVal stores As ToolStripMenuItem)
        Dim mnuStockOrders As ToolStripMenuItem
        Dim mnuWarehouseStockTakes As ToolStripMenuItem
        Dim mnuWarehouseStockTransfer As ToolStripMenuItem
        Dim mnuInterbranch As ToolStripMenuItem
        Dim mnuReturnSupplier As ToolStripMenuItem
        Dim mnuSepStoresInventory1 As ToolStripSeparator

        'Stores Menu
        mnuStockOrders = New ToolStripMenuItem
        With mnuStockOrders
            .Text = "Stock Orders"
            .Name = "mnuStockOrders"
        End With

        mnuWarehouseStockTakes = New ToolStripMenuItem
        With mnuWarehouseStockTakes
            .Text = "Warehouse StockTakes"
            .Name = "mnuWarehouseStockTakes"
        End With

        mnuInterbranch = New ToolStripMenuItem
        With mnuInterbranch
            .Text = "Interbranch"
            .Name = "mnuInterbranch"
        End With


        mnuWarehouseStockTransfer = New ToolStripMenuItem
        With mnuWarehouseStockTransfer
            .Text = "Warehouse Stock Transfer"
            .Name = "mnuWarehouseStockTransfer"
        End With


        mnuSepStoresInventory1 = New ToolStripSeparator
        With mnuSepStoresInventory1
            .Name = "mnuSepStoresInventory1"
        End With

        mnuReturnSupplier = New ToolStripMenuItem
        With mnuReturnSupplier
            .Text = "Return Supplier"
            .Name = "mnuReturnSupplier"
        End With


        'Warehouse
        'stores.DropDownItems.Add(mnuStockOrders)
        stores.DropDownItems.Add(mnuWarehouseStockTakes)
        stores.DropDownItems.Add(mnuInterbranch)
        stores.DropDownItems.Add(mnuWarehouseStockTransfer)
        'stores.DropDownItems.Add(mnuSepStoresInventory1)
        'stores.DropDownItems.Add(mnuReturnSupplier)

        DropMenuWarehouseStockTakes(mnuWarehouseStockTakes)
        DropMenuInterbranch(mnuInterbranch)
        DropMenuWarehouseStockTransfer(mnuWarehouseStockTransfer)

    End Sub

    Private Sub DropMenuWarehouseStockTransfer(ByVal mnu As ToolStripMenuItem)
        Dim mnuWarehouseStockTransferIn As ToolStripMenuItem
        Dim mnuWarehouseStockTransferOut As ToolStripMenuItem

        mnuWarehouseStockTransferIn = New ToolStripMenuItem
        With mnuWarehouseStockTransferIn
            .Text = "In"
            .Name = "mnuWarehouseStockTransferIn"
        End With

        mnuWarehouseStockTransferOut = New ToolStripMenuItem
        With mnuWarehouseStockTransferOut
            .Text = "Out"
            .Name = "mnuWarehouseStockTransferOut"
        End With

        AddHandler mnuWarehouseStockTransferIn.Click, AddressOf MenuItemClicked
        mnu.DropDownItems.Add(mnuWarehouseStockTransferIn)
        AddHandler mnuWarehouseStockTransferOut.Click, AddressOf MenuItemClicked
        mnu.DropDownItems.Add(mnuWarehouseStockTransferOut)
    End Sub

    Private Sub DropMenuVouchers(ByVal mnu As ToolStripMenuItem)
        Dim mnuVoucherBatch As ToolStripMenuItem

        mnuVoucherBatch = New ToolStripMenuItem
        With mnuVoucherBatch
            .Text = "Voucher Batch Issue"
            .Name = "mnuVoucherBatch"
        End With

        AddHandler mnuVoucherBatch.Click, AddressOf MenuItemClicked
        mnu.DropDownItems.Add(mnuVoucherBatch)
    End Sub

    Private Sub DropMenuInterbranch(ByVal mnu As ToolStripMenuItem)
        Dim mnuInterbranchIn As ToolStripMenuItem
        Dim mnuInterbranchOut As ToolStripMenuItem

        mnuInterbranchIn = New ToolStripMenuItem
        With mnuInterbranchIn
            .Text = "In"
            .Name = "mnuInterbranchIn"
        End With

        mnuInterbranchOut = New ToolStripMenuItem
        With mnuInterbranchOut
            .Text = "Out"
            .Name = "mnuInterbranchOut"
        End With

        AddHandler mnuInterbranchIn.Click, AddressOf MenuItemClicked
        mnu.DropDownItems.Add(mnuInterbranchIn)
        AddHandler mnuInterbranchOut.Click, AddressOf MenuItemClicked
        mnu.DropDownItems.Add(mnuInterbranchOut)
    End Sub

    Private Sub DropMenuWarehouseStockTakes(ByVal mnu As ToolStripMenuItem)
        Dim mnuWarehouseStockTakesCredit As ToolStripMenuItem
        Dim mnuWarehouseStockTakesConsignment As ToolStripMenuItem

        mnuWarehouseStockTakesCredit = New ToolStripMenuItem
        With mnuWarehouseStockTakesCredit
            .Text = "Credit"
            .Name = "mnuWarehouseStockTakesCredit"
        End With

        mnuWarehouseStockTakesConsignment = New ToolStripMenuItem
        With mnuWarehouseStockTakesConsignment
            .Text = "Consignment"
            .Name = "mnuWarehouseStockTakesConsignment"
        End With

        AddHandler mnuWarehouseStockTakesCredit.Click, AddressOf MenuItemClicked
        mnu.DropDownItems.Add(mnuWarehouseStockTakesCredit)
        AddHandler mnuWarehouseStockTakesConsignment.Click, AddressOf MenuItemClicked
        mnu.DropDownItems.Add(mnuWarehouseStockTakesConsignment)
    End Sub

    Private Sub DropMenuSalesReport(ByVal mnu As ToolStripMenuItem)
        Dim mnuHeaderStores As ToolStripMenuItem
        Dim mnuDailySales As ToolStripMenuItem
        Dim mnuMonthlySales As ToolStripMenuItem
        Dim mnuSepSalesReport1 As ToolStripSeparator
        Dim mnuBestSeller As ToolStripMenuItem
        Dim mnuListingSales As ToolStripMenuItem
        Dim mnuSalesProduct As ToolStripMenuItem
        Dim mnuSummarySalesProduct As ToolStripMenuItem
        Dim mnuSepSalesReport2 As ToolStripSeparator
        Dim mnuSummarySalesItem As ToolStripMenuItem
        Dim mnuDailySalesPayment As ToolStripMenuItem
        Dim mnuHeaderSupplier As ToolStripMenuItem
        Dim mnuSalesConsignment As ToolStripMenuItem
        Dim mnuSalesSupplier As ToolStripMenuItem
        Dim mnuBaseOnSupplier As ToolStripMenuItem

        'Sales Menu
        mnuHeaderStores = New ToolStripMenuItem
        With mnuHeaderStores
            .Text = "Stores"
            .Name = "mnuHeaderStores"
            .Font = New Font("Tahoma", 9, FontStyle.Bold)
            .Enabled = False
        End With

        mnuDailySales = New ToolStripMenuItem
        With mnuDailySales
            .Text = "Daily Sales"
            .Name = "mnuDailySales"
        End With

        mnuMonthlySales = New ToolStripMenuItem
        With mnuMonthlySales
            .Text = "Monthly Sales"
            .Name = "mnuMonthlySales"
        End With

        mnuSepSalesReport1 = New ToolStripSeparator
        With mnuSepSalesReport1
            .Name = "mnuSepSalesReport1"
        End With

        mnuBestSeller = New ToolStripMenuItem
        With mnuBestSeller
            .Text = "Best Seller"
            .Name = "mnuBestSeller"
        End With

        mnuListingSales = New ToolStripMenuItem
        With mnuListingSales
            .Text = "Listing Sales"
            .Name = "mnuListingSales"
        End With

        mnuSalesProduct = New ToolStripMenuItem
        With mnuSalesProduct
            .Text = "Sales Product"
            .Name = "mnuSalesProduct"
        End With

        mnuSummarySalesProduct = New ToolStripMenuItem
        With mnuSummarySalesProduct
            .Text = "Summary Sales Product"
            .Name = "mnuSummarySalesProduct"
        End With

        mnuSepSalesReport2 = New ToolStripSeparator
        With mnuSepSalesReport2
            .Name = "mnuSepSalesReport2"
        End With

        mnuSummarySalesItem = New ToolStripMenuItem
        With mnuSummarySalesItem
            .Text = "Summary Sales by Item"
            .Name = "mnuSummarySalesItem"
        End With

        mnuDailySalesPayment = New ToolStripMenuItem
        With mnuDailySalesPayment
            .Text = "Daily Sales Payments"
            .Name = "mnuDailySalesPayment"
        End With

        mnuHeaderSupplier = New ToolStripMenuItem
        With mnuHeaderSupplier
            .Text = "Suppliers"
            .Name = "mnuHeaderSupplier"
            .Font = New Font("Tahoma", 9, FontStyle.Bold)
            .Enabled = False
        End With

        mnuSalesConsignment = New ToolStripMenuItem
        With mnuSalesConsignment
            .Text = "Sales Consignment"
            .Name = "mnuSalesConsignment"
        End With

        mnuSalesSupplier = New ToolStripMenuItem
        With mnuSalesSupplier
            .Text = "Sales Supplier"
            .Name = "mnuSalesSupplier"
        End With

        mnuBaseOnSupplier = New ToolStripMenuItem
        With mnuBaseOnSupplier
            .Text = "Base On Supplier - Sales"
            .Name = "mnuBaseOnSupplier"
        End With

        'Sales Report
        mnu.DropDownItems.Add(mnuHeaderStores)
        'mnu.DropDownItems.Add(mnuDailySales)
        'mnu.DropDownItems.Add(mnuMonthlySales)
        'mnu.DropDownItems.Add(mnuSepSalesReport1)

        AddHandler mnuBestSeller.Click, AddressOf MenuItemClicked
        mnu.DropDownItems.Add(mnuBestSeller)
        AddHandler mnuListingSales.Click, AddressOf MenuItemClicked
        mnu.DropDownItems.Add(mnuListingSales)
        AddHandler mnuSalesProduct.Click, AddressOf MenuItemClicked
        mnu.DropDownItems.Add(mnuSalesProduct)

        AddHandler mnuSummarySalesProduct.Click, AddressOf MenuItemClicked
        mnu.DropDownItems.Add(mnuSummarySalesProduct)

        AddHandler mnuSummarySalesItem.Click, AddressOf MenuItemClicked
        mnu.DropDownItems.Add(mnuSummarySalesItem)
        AddHandler mnuDailySalesPayment.Click, AddressOf MenuItemClicked
        mnu.DropDownItems.Add(mnuDailySalesPayment)
        mnu.DropDownItems.Add(mnuSepSalesReport2)
        
        'AddHandler mnuSalesConsignment.Click, AddressOf MenuItemClicked
        'mnu.DropDownItems.Add(mnuSalesConsignment)
        mnu.DropDownItems.Add(mnuHeaderSupplier)
        AddHandler mnuSalesSupplier.Click, AddressOf MenuItemClicked
        mnu.DropDownItems.Add(mnuSalesSupplier)

        AddHandler mnuBaseOnSupplier.Click, AddressOf MenuItemClicked
        mnu.DropDownItems.Add(mnuBaseOnSupplier)


    End Sub

    Private Sub DropMenuInventoryReport(ByVal mnu As ToolStripMenuItem)
        Dim mnuHeaderStores As ToolStripMenuItem
        Dim mnuStoreStockLevel As ToolStripMenuItem
        Dim mnuWarehouseStockLevel As ToolStripMenuItem
        Dim mnuListingWarehouseReceive As ToolStripMenuItem
        Dim mnuListingWarehouseMovement As ToolStripMenuItem
        Dim mnuInventoryDemand As ToolStripMenuItem
        Dim mnuHeaderSupplier As ToolStripMenuItem
        Dim mnuInventorySupplier As ToolStripMenuItem
        Dim mnuStockAgingSupplier As ToolStripMenuItem
        Dim mnuBaseOnSupplierInventory As ToolStripMenuItem

        'Inventory Menu

        mnuHeaderStores = New ToolStripMenuItem
        With mnuHeaderStores
            .Text = "Stores"
            .Name = "mnuHeaderStores"
            .Font = New Font("Tahoma", 9, FontStyle.Bold)
            .Enabled = False

        End With

        mnuStoreStockLevel = New ToolStripMenuItem
        With mnuStoreStockLevel
            .Text = "Store Stock Level"
            .Name = "mnuStoreStockLevel"
        End With

        mnuWarehouseStockLevel = New ToolStripMenuItem
        With mnuWarehouseStockLevel
            .Text = "Warehouse Stock Level"
            .Name = "mnuWarehouseStockLevel"
        End With

        mnuListingWarehouseReceive = New ToolStripMenuItem
        With mnuListingWarehouseReceive
            .Text = "Listing Warehouse Receive"
            .Name = "mnuListingWarehouseReceive"
        End With

        mnuListingWarehouseMovement = New ToolStripMenuItem
        With mnuListingWarehouseMovement
            .Text = "Listing Warehouse Movement"
            .Name = "mnuListingWarehouseMovement"
        End With

        mnuInventoryDemand = New ToolStripMenuItem
        With mnuInventoryDemand
            .Text = "Inventory Demand Status"
            .Name = "mnuInventoryDemand"
        End With

        mnuHeaderSupplier = New ToolStripMenuItem
        With mnuHeaderSupplier
            .Text = "Suppliers"
            .Name = "mnuHeaderSupplier"
            .Font = New Font("Tahoma", 9, FontStyle.Bold)
            .Enabled = False
        End With

        mnuInventorySupplier = New ToolStripMenuItem
        With mnuInventorySupplier
            .Text = "Inventory Supplier"
            .Name = "mnuInventorySupplier"
        End With

        mnuStockAgingSupplier = New ToolStripMenuItem
        With mnuStockAgingSupplier
            .Text = "Stock Aging Supplier"
            .Name = "mnuStockAgingSupplier"
        End With

        mnuBaseOnSupplierInventory = New ToolStripMenuItem
        With mnuBaseOnSupplierInventory
            .Text = "Base On Supplier - Inventory"
            .Name = "mnuBaseOnSupplierInventory"
        End With
        'Inventory Report
        'mnu.DropDownItems.Add(mnuStoreStockLevel)
        mnu.DropDownItems.Add(mnuHeaderStores)
        mnu.DropDownItems.Add(mnuWarehouseStockLevel)
        mnu.DropDownItems.Add(mnuListingWarehouseReceive)
        mnu.DropDownItems.Add(mnuListingWarehouseMovement)
        mnu.DropDownItems.Add(mnuInventoryDemand)
        mnu.DropDownItems.Add(mnuHeaderSupplier)
        mnu.DropDownItems.Add(mnuInventorySupplier)
        mnu.DropDownItems.Add(mnuStockAgingSupplier)
        mnu.DropDownItems.Add(mnuBaseOnSupplierInventory)

        'AddHandler mnuStoreStockLevel.Click, AddressOf MenuItemClicked
        AddHandler mnuWarehouseStockLevel.Click, AddressOf MenuItemClicked
        AddHandler mnuListingWarehouseReceive.Click, AddressOf MenuItemClicked
        AddHandler mnuListingWarehouseMovement.Click, AddressOf MenuItemClicked
        AddHandler mnuInventoryDemand.Click, AddressOf MenuItemClicked
        AddHandler mnuInventorySupplier.Click, AddressOf MenuItemClicked
        AddHandler mnuStockAgingSupplier.Click, AddressOf MenuItemClicked
        AddHandler mnuBaseOnSupplierInventory.Click, AddressOf MenuItemClicked
    End Sub

    Private Sub MenuItemClicked(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim mnuItem As String = DirectCast(sender, ToolStripItem).Name
        Try
            Select Case mnuItem
                Case "mnuPOS"

                    If Not logOn = "00-IT" Then
                        table = New DataTable
                        table = ValidateOpenCashier()

                        If table.Rows.Count = 0 Then
                            MsgBox("Please Open Cashier First!", MsgBoxStyle.Exclamation, Title)
                            Exit Sub
                        End If
                    End If

                    'check valid userid and employee id cashier
                    If Not logOn = "00-IT" Then
                        Dim fValidateEmp As frmValidateMember = New frmValidateMember

                        fValidateEmp.FormState = 1
                        If fValidateEmp.ShowDialog = DialogResult.OK Then
                            If fValidateEmp.GetEmpIDValid = False Then
                                MsgBox("Userid and Employee ID not valid!!", MsgBoxStyle.Exclamation, Title)
                                Exit Sub
                            End If
                        Else
                            Exit Sub
                        End If
                    End If


                    If logOn = "00-IT" Then
                        frmPOS.EmployeeID = "Admin"
                    Else
                        frmPOS.EmployeeID = Trim(table.Rows(0).Item(1))

                    End If

                    frmPOS.Default_Customer = Trim(GetValueParamText("POS CUSTOMER"))
                    frmPOS.Default_SlsOrg = Trim(GetValueParamText("POS SLSORG"))
                    frmPOS.Default_SalesOffice = Trim(GetValueParamText("POS SALESOFFICE"))
                    frmPOS.Default_CostCenter = Trim(GetValueParamText("POS COSTCENTER"))
                    frmPOS.Default_Salesman = Trim(GetValueParamText("POS SALESMAN"))
                    frmPOS.Default_Warehouse = Trim(GetValueParamText("DEFAULT WH"))

                    frmPOS.MdiParent = MDIMain
                    frmPOS.WindowState = FormWindowState.Maximized
                    frmPOS.Show()

                    AddHandler frmPOS.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Sales"

                Case "mnuExhibition"

                    If Not logOn = "00-IT" Then
                        table = New DataTable
                        table = ValidateOpenCashier()

                        If table.Rows.Count = 0 Then
                            MsgBox("Please Open Cashier First!", MsgBoxStyle.Exclamation, Title)
                            Exit Sub
                        End If
                    End If

                    'check valid userid and employee id cashier
                    If Not logOn = "00-IT" Then
                        Dim fValidateEmp As frmValidateMember = New frmValidateMember

                        fValidateEmp.FormState = 1
                        If fValidateEmp.ShowDialog = DialogResult.OK Then
                            If fValidateEmp.GetEmpIDValid = False Then
                                MsgBox("Userid and Employee ID not valid!!", MsgBoxStyle.Exclamation, Title)
                                Exit Sub
                            Else
                                MsgBox("Userid and Employee ID valid!!", MsgBoxStyle.Information, Title)
                            End If
                        End If
                    End If


                    If logOn = "00-IT" Then
                        frmPOS.EmployeeID = "Admin"
                    Else
                        frmPOS.EmployeeID = Trim(table.Rows(0).Item(1))

                    End If

                    frmPOS.Text = "EXHIBITION (PAMERAN)"
                    frmPOS.Default_Customer = Trim(GetValueParamText("PAM CUSTOMER"))
                    frmPOS.Default_SlsOrg = Trim(GetValueParamText("PAM SLSORG"))
                    frmPOS.Default_SalesOffice = Trim(GetValueParamText("PAM SALESOFFICE"))
                    frmPOS.Default_CostCenter = Trim(GetValueParamText("PAM COSTCENTER"))
                    frmPOS.Default_Salesman = Trim(GetValueParamText("PAM SALESMAN"))
                    frmPOS.Default_Warehouse = Trim(GetValueParamText("PAM WAREHOUSE"))

                    frmPOS.MdiParent = MDIMain
                    frmPOS.WindowState = FormWindowState.Maximized
                    frmPOS.Show()

                    AddHandler frmPOS.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Sales"

                Case "mnuOpenCashier"

                    table = New DataTable
                    table = ValidateOpenCashier()

                    If table.Rows.Count > 0 Then
                        MsgBox("Please Close Cashier First!", MsgBoxStyle.Exclamation, Title)
                        Exit Sub
                    End If

                    frm = frmOpenCashier

                    frm.ShowDialog()

                    MDIMain.Text = "TMBookstore - Open Cashier"
                Case "mnuCloseCashier"

                    table = New DataTable
                    table = ValidateOpenCashier()

                    Dim child As Form

                    For Each child In MDIMain.MdiChildren
                        If child.Name = "frmPOS" Then
                            MsgBox("POS active, please close this form first!!!", MsgBoxStyle.Exclamation, Title)

                            Exit Sub
                        End If
                    Next

                    If Not table.Rows.Count > 0 Then
                        MsgBox("Please Open Cashier First!", MsgBoxStyle.Exclamation, Title)
                        Exit Sub
                    End If

                    frm = frmCloseCashier

                    frm.ShowDialog()

                    MDIMain.Text = "TMBookstore - Sales"
                Case "mnuPosEndofDay"
                    frm = frmPosEndDay

                    frm.ShowDialog()

                    MDIMain.Text = "TMBookstore - Finance"
                Case "mnuWarehouseStockTakesCredit"
                    Dim fWR As New FrmWR

                    fWR.MdiParent = MDIMain
                    fWR.FromWH = GetValueParamText("DEFAULT WH")
                    fWR.WRTitle = "Warehouse Stock Takes Credit"
                    fWR.WRTransID = "GR101"
                    fWR.WRFlag = 1
                    fWR.WindowState = FormWindowState.Maximized
                    fWR.Show()

                    AddHandler fWR.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Warehouse Stock Takes "

                Case "mnuWarehouseStockTakesConsignment"
                    Dim fWR As New FrmWR
                    fWR.MdiParent = MDIMain
                    fWR.FromWH = GetValueParamText("DEFAULT WH")
                    fWR.WRTitle = "Warehouse Stock Takes Consignment"
                    fWR.WRTransID = "GR102"
                    fWR.WRFlag = 1
                    fWR.WindowState = FormWindowState.Maximized
                    fWR.Show()

                    AddHandler fWR.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Warehouse Stock Takes "

                Case "mnuWarehouseStockTransferIn"
                    Dim fWR As New FrmWR

                    fWR.MdiParent = MDIMain
                    fWR.FromWH = GetValueParamText("DEFAULT WH")
                    fWR.WRTitle = "Warehouse Stock Transfer In"
                    fWR.WRTransID = "TR100"
                    fWR.WRFlag = 0
                    fWR.WindowState = FormWindowState.Maximized
                    fWR.Show()

                    AddHandler fWR.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Warehouse Stock Transfer"
                Case "mnuInterbranchIn"
                    Dim fWR As New FrmWR

                    fWR.MdiParent = MDIMain
                    fWR.FromWH = GetValueParamText("DEFAULT WH")
                    fWR.WRTitle = "Interbranch In"
                    fWR.WRTransID = "GR410"
                    fWR.WRFlag = 0
                    fWR.WindowState = FormWindowState.Maximized
                    fWR.Show()

                    AddHandler fWR.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Inventory"

                Case "mnuWarehouseStockTransferOut"
                    Dim f As New frmTransfer

                    f.MdiParent = MDIMain
                    f.FromWH = GetValueParamText("DEFAULT WH")
                    f.TransferTitle = "Warehouse Stock Transfer Out"
                    f.TransID = "MM106"
                    f.TransferFlag = 0
                    f.WindowState = FormWindowState.Maximized
                    f.Show()

                    AddHandler f.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Inventory"
                Case "mnuApproval"
                    frm = frmApproval
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Report"
                Case "mnuBestSeller"
                    frm = frmBestSeller
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Report"
                Case "mnuListingSales"
                    frm = frmListingSales
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Report"
                Case "mnuSalesProduct"
                    frm = frmSalesProducts
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Report"
                Case "mnuSalesSupplier"
                    frm = frmSalesSupplier
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Report"
                Case "mnuBaseOnSupplier"
                    frm = frmBaseonSupplier
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Report"
                Case "mnuSummarySalesItem"
                    frm = frmSummarySalesItem
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Sales"

                Case "mnuSummarySalesProduct"
                    frm = frmSummarySalesProduct
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Sales"

                Case "mnuSalesConsignment"
                    frm = frmSalesConsignment
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Sales"
                Case "mnuDailySalesPayment"
                    frm = frmDailySalesPayments
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Sales"

                Case "mnuClosingCashier"
                    frm = frmClosingCashier
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Finance"

                Case "mnuWarehouseStockLevel"
                    frm = frmWarehouseStockLevel
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Reports"
                Case ("mnuListingWarehouseReceive")
                    frm = frmListingWarehouseReceive
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Reports"
                Case "mnuListingWarehouseMovement"
                    frm = frmListingWarehouseMovement
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Reports"
                Case "mnuInventoryDemand"
                    frm = frmInventoryDemandStatus
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Reports"
                Case "mnuInventorySupplier"
                    frm = frmInventorySupplier
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Reports"
                Case "mnuStockAgingSupplier"
                    frm = frmStockAgingSupplier
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Reports"
                Case "mnuBaseOnSupplierInventory"
                    frm = frmBaseOnSupplier2
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Reports"
                Case "mnuEmployees"
                    frm = frmEmployee
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - HRD/GA"

                Case "mnuProducts"
                    frm = frmProducts
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Products"

                Case "mnuEvent"
                    frm = frmMasterEvent
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Products"

                Case "mnuVoucherBatch"
                    frm = frmVoucher
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Products"
                Case "mnuDisplay"
                    frm = frmDisplay
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Products"
                Case "mnuParameter"

                    frm = FrmParameter
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Setup"

                Case "mnuGetLastReceive"
                    frm = frmGetLastReceive
                    frm.ShowDialog()

                    MDIMain.Text = "TMBookstore - Tools"

                Case "mnuRealStock"
                    frm = frmRealStock
                    frm.ShowDialog()

                    MDIMain.Text = "TMBookstore - Tools"
                Case "mnuMenuAccess"

                    frm = frmMenuAccess
                    frm.ShowDialog()

                    MDIMain.Text = "TMBookstore - Setup"
                Case "mnuDatabase"

                    frm = frmSetupSQL
                    frm.ShowDialog()

                    MDIMain.Text = "TMBookstore - Database"
                Case "mnuUsers"

                    If Not logOn = "00-IT" Then

                        MsgBox("You don't have access to this menu!!!", MsgBoxStyle.Exclamation, Title)
                        Exit Sub

                    End If


                    frm = frmUserAdmin
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()

                    AddHandler frm.FormClosed, AddressOf ActiveMdiChild_FormClosed
                    MDIMain.Text = "TMBookstore - Users"

                Case "mnuChangePassword"
                    frm = frmChangePassword
                    MDIMain.Text = "TMBookstore - Change Password"
                    frm.ShowDialog()

                Case "mnuInterfacing"
                    frm = frmInterfacing
                    frm.MdiParent = MDIMain
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()
                    MDIMain.Text = "TMBookstore - Interfacing Information"
            End Select

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)

        End Try
    End Sub

    Public Sub ActiveMdiChild_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
        TryCast(TryCast(sender, Form).Tag, TabPage).Dispose()
    End Sub


End Module
