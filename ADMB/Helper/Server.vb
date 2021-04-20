Imports System.Data.SQLite
Imports System.IO
Imports NPOI.HPSF
Imports NPOI.HSSF.UserModel

Public Class Server
    Dim location As String = System.AppDomain.CurrentDomain.BaseDirectory
    Dim filename As String = ("database.db")
    Dim fullpath As String = System.IO.Path.Combine(location, filename)
    Public connectionstr As String = String.Format("Data Source = {0}", fullpath)
    Public Sub createdatabase()
        If Not duplicatedatabase(fullpath) Then
            Dim createtable As String = "CREATE TABLE `AuctionChannels` (
	`ID`	INTEGER,
	`Serverid`	INTEGER,
	`Channelid`	INTEGER,
	PRIMARY KEY(`ID` AUTOINCREMENT)
);
CREATE TABLE `AuctionStatus` (
	`ID`	INTEGER,
	`Serverid`	INTEGER,
	`Status`	TEXT,
	`role`	INTEGER,
	PRIMARY KEY(`ID` AUTOINCREMENT)
);
CREATE TABLE `Auctions` (
	`ID`	INTEGER,
	`Channelid`	INTEGER,
	`Serverid`	INTEGER,
	`msgid`	INTEGER,
	`embedid`	INTEGER,
	`item`	TEXT,
	`description`	TEXT,
	`payment`	TEXT,
	`startingbid`	INTEGER,
	`bidinc`	INTEGER,
	`time`	TEXT DEFAULT 'a',
	`link`	TEXT,
	`snipeguard`	TEXT,
	`openerid`	INTEGER DEFAULT 0,
	`openerlink`	TEXT DEFAULT 'nothing',
	`firstbid`	INTEGER DEFAULT 0,
	`lastbidid`	INTEGER DEFAULT 0,
	`requestedby`	TEXT,
	`lastbidname`	TEXT DEFAULT 'a',
	PRIMARY KEY(`ID` AUTOINCREMENT)
);
CREATE TABLE `Blacklist` (
	`ID`	INTEGER,
	`Serverid`	INTEGER,
	`Reason`	TEXT,
	`Bannedby`	TEXT,
	PRIMARY KEY(`ID` AUTOINCREMENT)
);
CREATE TABLE `Breeders` (
	`ID`	INTEGER,
	`Breederid`	INTEGER,
	`Tame`	TEXT,
	`serverlink`	TEXT,
	`Breedername`	TEXT,
	PRIMARY KEY(`ID` AUTOINCREMENT)
);
CREATE TABLE `CurrencyRates` (
	`ID`	INTEGER,
	`Serverid`	INTEGER,
	`Money`	INTEGER DEFAULT 1,
	`Tek`	INTEGER DEFAULT 'NoN',
	`Ingots`	INTEGER DEFAULT 'NoN',
	`Poly`	INTEGER DEFAULT 'NoN',
	`Crystal`	INTEGER DEFAULT 'NoN',
	`CP`	INTEGER DEFAULT 'NoN',
	PRIMARY KEY(`ID` AUTOINCREMENT)
);
CREATE TABLE `CurrencyRatesStatus` (
	`ID`	INTEGER,
	`Serverid`	INTEGER,
	PRIMARY KEY(`ID` AUTOINCREMENT)
);
CREATE TABLE `Customers` (
	`ID`	INTEGER,
	`Serverid`	INTEGER,
	`item`	TEXT,
	`orderpostid`	INTEGER,
	`authorid`	INTEGER,
	`authorname`	TEXT,
	PRIMARY KEY(`ID` AUTOINCREMENT)
);
CREATE TABLE `Customersorder` (
	`ID`	INTEGER,
	`Serverid`	INTEGER,
	`Userid`	INTEGER,
	`Orders`	INTEGER DEFAULT 0,
	`Nickname`	TEXT DEFAULT 'Empty',
	`ProcessorName`	TEXT DEFAULT 'Empty',
	`ProcessorID`	INTEGER DEFAULT 0,
	`Lastorderid`	INTEGER DEFAULT 0,
	`Lastorderlist`	TEXT DEFAULT 'Empty',
	PRIMARY KEY(`ID` AUTOINCREMENT)
);
CREATE TABLE `EpicPl` (
	`ID`	INTEGER,
	`Memberid`	INTEGER,
	`Epicid`	TEXT,
	PRIMARY KEY(`ID` AUTOINCREMENT)
);
CREATE TABLE `Glchatmuted` (
	`ID`	INTEGER,
	`Userid`	INTEGER,
	PRIMARY KEY(`ID` AUTOINCREMENT)
);
CREATE TABLE `Globalchat` (
	`ID`	INTEGER,
	`Serverid`	INTEGER,
	`Channelid`	INTEGER,
	PRIMARY KEY(`ID` AUTOINCREMENT)
);
CREATE TABLE `Money` (
	`ID`	INTEGER,
	`Serverid`	INTEGER,
	`Userid`	INTEGER,
	`Amount`	REAL DEFAULT 0,
	`LastCheck`	TEXT DEFAULT 0,
	PRIMARY KEY(`ID` AUTOINCREMENT)
);
CREATE TABLE `MoneyStatus` (
	`ID`	INTEGER,
	`Serverid`	INTEGER,
	`Daily`	INTEGER DEFAULT 0,
	`Points`	INTEGER DEFAULT 0,
	PRIMARY KEY(`ID` AUTOINCREMENT)
);
CREATE TABLE `OrderTemplates` (
	`ID`	INTEGER,
	`Serverid`	TEXT,
	`Template`	TEXT,
	`Name`	TEXT,
	PRIMARY KEY(`ID` AUTOINCREMENT)
);
CREATE TABLE `Orders` (
	`ID`	INTEGER,
	`Serverid`	INTEGER,
	`Postchannel`	INTEGER,
	`Submitchannel`	INTEGER,
	`Rolemention`	TEXT,
	`Servername`	TEXT,
	`Track`	TEXT DEFAULT 'False',
	`Channelid`	INTEGER DEFAULT 'False',
	PRIMARY KEY(`ID`)
);
CREATE TABLE `OrdersList` (
	`ID`	INTEGER,
	`Serverid`	INTEGER,
	`Channelid`	INTEGER,
	`item`	TEXT,
	`charactername`	TEXT,
	`payment`	TEXT,
	`note`	TEXT,
	`orderpostid`	INTEGER,
	`authorid`	INTEGER,
	`time`	INTEGER DEFAULT 0,
	`submittime`	TEXT DEFAULT 0,
	PRIMARY KEY(`ID` AUTOINCREMENT)
);
CREATE TABLE `Paid` (
	`ID`	INTEGER,
	`Serverid`	INTEGER,
	`Amount`	INTEGER,
	`Starteddate`	TEXT,
	PRIMARY KEY(`ID` AUTOINCREMENT)
);
CREATE TABLE `Partner` (
	`ID`	INTEGER,
	`Serverid`	NUMERIC,
	`Servername`	TEXT,
	`Serverdescription`	TEXT,
	`Serverlink`	TEXT,
	`timestamp`	TEXT,
	`Iconurl`	TEXT,
	`Channelid`	INTEGER,
	PRIMARY KEY(`ID` AUTOINCREMENT)
);
CREATE TABLE `Reports` (
	`ID`	INTEGER,
	`Reportby`	TEXT,
	`Reportedserver`	TEXT,
	`Reason`	TEXT,
	PRIMARY KEY(`ID` AUTOINCREMENT)
);
CREATE TABLE `ServerStatus` (
	`ID`	INTEGER,
	`Serverid`	INTEGER,
	PRIMARY KEY(`ID` AUTOINCREMENT)
);
CREATE TABLE `Setups` (
	`ID`	INTEGER,
	`Serverid`	INTEGER,
	`Logs`	INTEGER DEFAULT 0,
	`Is1channel`	INTEGER DEFAULT 0,
	`Abanasync`	TEXT DEFAULT 'False',
	PRIMARY KEY(`ID` AUTOINCREMENT)
);
CREATE TABLE `Userban` (
	`ID`	INTEGER,
	`Serverid`	INTEGER,
	`Userid`	INTEGER,
	`Reason`	TEXT,
	`BannedBy`	TEXT,
	PRIMARY KEY(`ID` AUTOINCREMENT)
);
"
            Using sqlconn As New SQLiteConnection(connectionstr)
                Dim cmd As New SQLiteCommand(createtable, sqlconn)

                sqlconn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End If
    End Sub
    Public Sub insertorder(ByVal serverid As Long, ByVal postchannel As Long, ByVal submitchannel As Long, ByVal rolemention As Long, ByVal Servername As String, ByVal Track As Boolean, ByVal Channelid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "INSERT INTO Orders(Serverid,Postchannel,Submitchannel,Rolemention,Servername,Track,Channelid) VALUES (@Serverid, @Postchannel, @Submitchannel, @Rolemention, @Servername,@Track,@Channelid)"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@Postchannel", postchannel)
            cmd.Parameters.AddWithValue("@Submitchannel", submitchannel)
            cmd.Parameters.AddWithValue("@Rolemention", rolemention)
            cmd.Parameters.AddWithValue("@Servername", Servername)
            cmd.Parameters.AddWithValue("@Track", Track.ToString)
            cmd.Parameters.AddWithValue("@Channelid", Channelid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()

        End Using
    End Sub
    Public Function checkorderlistt(ByVal serverid As Long, ByVal orderpostid As Long, ByVal authorid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim kk As String = Nothing
            Dim insert As String = "SELECT authorid FROM OrdersList WHERE Serverid = @serverid AND orderpostid = @orderpostid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@orderpostid", orderpostid)
            sqlconn.Open()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())

                    If authorid = (reader("authorid")) Then
                        Return True

                    Else

                        Return False
                    End If
                End While

            End Using
        End Using
        Return Nothing
    End Function
    Public Function getnumberorderlist(ByVal serverid As Long, ByVal orderpostid As Long, ByVal authorid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim kk As String = Nothing
            Dim insert As String = "SELECT time FROM OrdersList WHERE Serverid = @serverid AND orderpostid = @orderpostid AND authorid = @authorid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@orderpostid", orderpostid)
            cmd.Parameters.AddWithValue("@authorid", authorid)
            sqlconn.Open()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())

                    Return (reader("time"))
                End While

            End Using
        End Using
        Return Nothing
    End Function
    Public Function checkiforderlistexist(ByVal serverid As Long, ByVal orderpostid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim kk As String = Nothing
            Dim insert As String = "SELECT * FROM OrdersList WHERE Serverid = @serverid AND orderpostid = @orderpostid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@orderpostid", orderpostid)
            sqlconn.Open()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())

                    If reader.HasRows Then
                        Return True

                    Else

                        Return False
                    End If
                End While

            End Using
        End Using
        Return Nothing
    End Function
    Public Sub updateordertolist(ByVal serverid As Long, ByVal channelid As Long, ByVal item As String, ByVal charactername As String, ByVal payment As String, ByVal note As String, ByVal orderpostid As Long, ByVal authorid As Long, ByVal time As Integer)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "UPDATE Orderslist SET Serverid=@serverid,Channelid=@channelid,item=@item,charactername=@charactername,payment=@payment,note=@note,orderpostid=@orderpostid,authorid=@authorid,time=@time WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@channelid", channelid)
            cmd.Parameters.AddWithValue("@item", item)
            cmd.Parameters.AddWithValue("@charactername", charactername)
            cmd.Parameters.AddWithValue("@payment", payment)
            cmd.Parameters.AddWithValue("@note", note)
            cmd.Parameters.AddWithValue("@orderpostid", orderpostid)
            cmd.Parameters.AddWithValue("@authorid", authorid)
            cmd.Parameters.AddWithValue("@time", time)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Sub clearorderlist(ByVal serverid As Long, ByVal int As Integer, Optional ByVal orderid As Long = 0)
        Select Case int
            Case 1
                Using sqlconn As New SQLiteConnection(connectionstr)
                    Dim insert As String = "DELETE FROM OrdersList WHERE Serverid=@serverid"
                    Dim cmd As New SQLiteCommand(insert, sqlconn)
                    cmd.Parameters.AddWithValue("@serverid", serverid)
                    sqlconn.Open()
                    cmd.ExecuteNonQuery()
                End Using
                Using sqlconn As New SQLiteConnection(connectionstr)
                    Dim insert As String = "DELETE FROM Customers WHERE Serverid=@serverid"
                    Dim cmd As New SQLiteCommand(insert, sqlconn)
                    cmd.Parameters.AddWithValue("@serverid", serverid)
                    sqlconn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            Case 2
                Using sqlconn As New SQLiteConnection(connectionstr)
                    Dim insert As String = "DELETE FROM OrdersList WHERE Serverid=@serverid AND orderpostid = @orderpostid"
                    Dim cmd As New SQLiteCommand(insert, sqlconn)
                    cmd.Parameters.AddWithValue("@serverid", serverid)
                    cmd.Parameters.AddWithValue("@orderpostid", orderid)
                    sqlconn.Open()
                    cmd.ExecuteNonQuery()
                End Using
                Using sqlconn As New SQLiteConnection(connectionstr)
                    Dim insert As String = "DELETE FROM Customers WHERE Serverid=@serverid AND orderpostid = @orderpostid"
                    Dim cmd As New SQLiteCommand(insert, sqlconn)
                    cmd.Parameters.AddWithValue("@serverid", serverid)
                    cmd.Parameters.AddWithValue("@orderpostid", orderid)
                    sqlconn.Open()
                    cmd.ExecuteNonQuery()
                End Using
        End Select
    End Sub
    Public Sub Insertordertolist(ByVal serverid As Long, ByVal channelid As Long, ByVal item As String, ByVal charactername As String, ByVal payment As String, ByVal note As String, ByVal orderpostid As Long, ByVal authorid As Long, ByVal submittime As String)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "INSERT INTO OrdersList(Serverid,Channelid,item,charactername,payment,note,orderpostid,authorid,submittime) VALUES (@Serverid, @Channelid, @item, @charactername, @payment, @note,@orderpostid,@authorid,@submittime)"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@Serverid", serverid)
            cmd.Parameters.AddWithValue("@Channelid", channelid)
            cmd.Parameters.AddWithValue("@item", item)
            cmd.Parameters.AddWithValue("@charactername", charactername)
            cmd.Parameters.AddWithValue("@payment", payment)
            cmd.Parameters.AddWithValue("@note", note)
            cmd.Parameters.AddWithValue("@orderpostid", orderpostid)
            cmd.Parameters.AddWithValue("@authorid", authorid)
            cmd.Parameters.AddWithValue("@submittime", submittime)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Function getsubmitchan(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT Submitchannel FROM Orders WHERE Serverid= @serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()
            channelidssss.Clear()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())
                    Return reader("Submitchannel")
                End While

            End Using
        End Using
        Return Nothing
    End Function
    Public Function Checkiforderexist(ByVal serverid As Long, ByVal authorid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM Customers WHERE Serverid=@serverid AND authorid=@authorid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@authorid", authorid)
            sqlconn.Open()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                If reader.HasRows Then
                    Return True
                Else
                    Return False
                End If
            End Using
        End Using

    End Function
    Public Function Checktrack(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT Track FROM Orders WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()
            Using reader As SQLiteDataReader = cmd.ExecuteReader

                While (reader.Read())
                    If reader("Track") = "False" Then
                        Return False
                    Else
                        Return True
                    End If
                End While
            End Using
        End Using

    End Function

    Public Function Checkifhaveprevious(ByVal serverid As Long, ByVal userid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM Customersorder WHERE Serverid=@serverid AND Userid=@userid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@Userid", userid)
            sqlconn.Open()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                If reader.HasRows Then
                    Return True
                Else
                    Return False
                End If
            End Using
        End Using

    End Function
    Public Sub Insertordertrack(ByVal serverid As Long, ByVal Userid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "INSERT INTO Customersorder(Serverid,Userid) VALUES (@Serverid,@Userid)"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@Serverid", serverid)
            cmd.Parameters.AddWithValue("@Userid", Userid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Sub Updatecustomerorders(ByVal serverid As Long, ByVal userid As String, ByVal orders As Long, ByVal Nickname As String, ByVal Processorname As String, ByVal processorid As Long, ByVal lastorderid As Long, ByVal lastorderlist As String)

        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "UPDATE Customersorder SET Orders=@orders,Nickname=@nickname,Processorname=@processorname,Processorid=@processorid,Lastorderid=@lastorderid,Lastorderlist=@lastorderlist WHERE Serverid=@serverid AND Userid = @userid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@userid", userid)
            cmd.Parameters.AddWithValue("@orders", orders)
            cmd.Parameters.AddWithValue("@nickname", Nickname)
            cmd.Parameters.AddWithValue("@processorname", Processorname)
            cmd.Parameters.AddWithValue("@processorid", processorid)
            cmd.Parameters.AddWithValue("@lastorderid", lastorderid)
            cmd.Parameters.AddWithValue("@lastorderlist", lastorderlist)
            sqlconn.Open()
            cmd.ExecuteNonQuery()


        End Using
    End Sub
    Private Shared hssfworkbook As HSSFWorkbook
    Public Function ExportCustomerData(ByVal serverid As Long, ByVal int As Integer)
        If int = 1 Then
            InitializeWorkbook(1)
            Dim sheet1 = hssfworkbook.GetSheet("Customers")
            Using sqlconn As New SQLiteConnection(connectionstr)
                Dim insert As String = "SELECT * FROM Customersorder WHERE Serverid=@serverid"
                Dim cmd As New SQLiteCommand(insert, sqlconn)
                cmd.Parameters.AddWithValue("@serverid", serverid)
                sqlconn.Open()
                Try

                    Using reader As SQLiteDataReader = cmd.ExecuteReader
                        Dim ID As Long = 1
                        Dim num As Long = 3
                        While (reader.Read())
                            Dim Userid As Long = reader("Userid")
                            Dim Username As String = reader("Nickname")
                            Dim LastOrderList As String = reader("Lastorderlist")
                            Dim LastOrderid As Long = reader("Lastorderid")
                            Dim ProcessorName As String = reader("ProcessorName")
                            Dim ProcessorID As Long = reader("ProcessorID")
                            Dim Orders As Long = reader("Orders")
                            Dim row1 As HSSFRow
                            row1 = sheet1.CreateRow(num)
                            row1.CreateCell(0).SetCellValue(ID)
                            row1.CreateCell(1)
                            row1.GetCell(1).SetCellValue(New HSSFRichTextString(Userid))
                            row1.CreateCell(2)
                            row1.GetCell(2).SetCellValue(New HSSFRichTextString($"<@{Userid}>"))
                            row1.CreateCell(3).SetCellValue(Username)
                            row1.CreateCell(4).SetCellValue(LastOrderList)
                            row1.CreateCell(5)
                            row1.GetCell(5).SetCellValue(New HSSFRichTextString(LastOrderid))
                            row1.CreateCell(6).SetCellValue(ProcessorName)
                            row1.CreateCell(7)
                            row1.GetCell(7).SetCellValue(New HSSFRichTextString(ProcessorID))
                            row1.CreateCell(8)
                            row1.GetCell(8).SetCellValue(New HSSFRichTextString($"<@{ProcessorID}>"))
                            row1.CreateCell(9).SetCellValue(Orders)
                            ID += 1
                            num += 1
                        End While


                    End Using
                    sheet1.ForceFormulaRecalculation = True
                    Return WriteToFile(serverid, 1)
                Catch ex As Exception
                End Try

            End Using
        ElseIf int = 2 Then
            InitializeWorkbook(2)
            Dim sheet1 = hssfworkbook.GetSheet("Customers")
            Using sqlconn As New SQLiteConnection(connectionstr)
                Dim insert As String = "SELECT * FROM Customers WHERE Serverid=@serverid"
                Dim cmd As New SQLiteCommand(insert, sqlconn)
                cmd.Parameters.AddWithValue("@serverid", serverid)
                sqlconn.Open()
                Try

                    Using reader As SQLiteDataReader = cmd.ExecuteReader
                        Dim ID As Long = 1
                        Dim num As Long = 3
                        While (reader.Read())
                            Dim Userid As Long = reader("authorid")
                            Dim Username As String = reader("authorname")
                            Dim Item As String = reader("item")
                            Dim Orderid As Long = reader("orderpostid")
                            Dim row1 As HSSFRow
                            row1 = sheet1.CreateRow(num)
                            row1.CreateCell(0).SetCellValue(ID)
                            row1.CreateCell(1)
                            row1.GetCell(1).SetCellValue(New HSSFRichTextString(Orderid))
                            row1.CreateCell(2).SetCellValue(Item)
                            row1.CreateCell(3).SetCellValue(Username)
                            row1.CreateCell(4)
                            row1.GetCell(4).SetCellValue(New HSSFRichTextString(Userid))
                            row1.CreateCell(5)
                            row1.GetCell(5).SetCellValue(New HSSFRichTextString($"<@{Userid}>"))
                            ID += 1
                            num += 1
                        End While


                    End Using
                    sheet1.ForceFormulaRecalculation = True
                    Return WriteToFile(serverid, 2)
                Catch ex As Exception
                    Console.WriteLine(ex.ToString)
                End Try

            End Using
        End If

        Return Nothing
    End Function
    Private Function WriteToFile(ByVal guildid As Long, ByVal int As Integer)
        If int = 1 Then
            Dim file As FileStream = New FileStream($"Customersodsheet/{guildid}.xls", FileMode.Create)
            hssfworkbook.Write(file)
            file.Close()
            Return $"Customersodsheet/{guildid}.xls"
        ElseIf int = 2 Then
            Dim file As FileStream = New FileStream($"Customerscodsheet/{guildid}.xls", FileMode.Create)
            hssfworkbook.Write(file)
            file.Close()
            Return $"Customerscodsheet/{guildid}.xls"
        End If
        Return Nothing
    End Function

    Private Function InitializeWorkbook(ByVal int As Integer)
        If int = 1 Then
            'read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            'book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            Dim file As FileStream = New FileStream("template/Customers - Completed Orders.xls", FileMode.Open, FileAccess.Read)
            hssfworkbook = New HSSFWorkbook(file)

            'create a entry of DocumentSummaryInformation
            Dim dsi As DocumentSummaryInformation = PropertySetFactory.CreateDocumentSummaryInformation()
            dsi.Company = "ADMB"
            hssfworkbook.DocumentSummaryInformation = dsi
            'create a entry of SummaryInformation
            Dim si As SummaryInformation = PropertySetFactory.CreateSummaryInformation()
            si.Subject = "ADMB Customers Sheet"
            hssfworkbook.SummaryInformation = si
        ElseIf int = 2 Then
            'read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            'book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            Dim file As FileStream = New FileStream("template/Customers - Current Orders.xls", FileMode.Open, FileAccess.Read)
            hssfworkbook = New HSSFWorkbook(file)

            'create a entry of DocumentSummaryInformation
            Dim dsi As DocumentSummaryInformation = PropertySetFactory.CreateDocumentSummaryInformation()
            dsi.Company = "ADMB"
            hssfworkbook.DocumentSummaryInformation = dsi
            'create a entry of SummaryInformation
            Dim si As SummaryInformation = PropertySetFactory.CreateSummaryInformation()
            si.Subject = "ADMB Customers Sheet"
            hssfworkbook.SummaryInformation = si
        End If

        Return Nothing
    End Function
    Public Function Getcutomersorder(ByVal serverid As Long, ByVal userid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM Customersorder WHERE Serverid=@serverid AND Userid = @userid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@userid", userid)
            sqlconn.Open()
            Using reader As SQLiteDataReader = cmd.ExecuteReader

                While (reader.Read())
                    Return (reader("orders"))
                End While
            End Using
        End Using

    End Function
    Public Sub Deleteuserorder(ByVal orderpostid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "DELETE FROM Customers WHERE orderpostid = @orderpostid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@orderpostid", orderpostid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Function GetCustomer(ByVal orderid As Long, ByVal i As Integer)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM Customers WHERE orderpostid=@orderpostid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@orderpostid", orderid)
            sqlconn.Open()
            Using reader As SQLiteDataReader = cmd.ExecuteReader

                While (reader.Read())
                    Select Case i
                        Case 1
                            Return (reader("item"))
                        Case 2
                            Return (reader("authorid"))
                        Case 3
                            Return (reader("orderpostid"))

                    End Select
                End While
            End Using
        End Using

    End Function
    Public Sub Updatetrackcustomer(ByVal serverid As Long, ByVal item As String, ByVal orderpostid As Long, ByVal authorid As Long, ByVal authorname As String)

        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "UPDATE Customers SET Serverid=@Serverid,item=@item,orderpostid=@orderpostid,authorid=@authorid,authorname = @authorname WHERE orderpostid=@orderpostid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@item", item)
            cmd.Parameters.AddWithValue("@orderpostid", orderpostid)
            cmd.Parameters.AddWithValue("@authorid", authorid)
            cmd.Parameters.AddWithValue("@authorname", authorname)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Sub Inserttrackcustomer(ByVal serverid As Long, ByVal item As String, ByVal orderpostid As Long, ByVal authorid As Long, ByVal authorname As String)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "INSERT INTO Customers(Serverid,item,orderpostid,authorid,authorname) VALUES (@Serverid, @item,@orderpostid,@authorid,@authorname)"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@Serverid", serverid)
            cmd.Parameters.AddWithValue("@item", item)
            cmd.Parameters.AddWithValue("@orderpostid", orderpostid)
            cmd.Parameters.AddWithValue("@authorid", authorid)
            cmd.Parameters.AddWithValue("@authorname", authorname)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Sub insertreport(ByVal reportedby As String, ByVal reportedserver As String, ByVal Reason As String)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "INSERT INTO Reports(Reportby,Reportedserver,Reason) VALUES ( @Reportedby, @Reportedserver, @Reason)"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@Reportedby", reportedby)
            cmd.Parameters.AddWithValue("@Reportedserver", reportedserver)
            cmd.Parameters.AddWithValue("@Reason", Reason)
            sqlconn.Open()
            cmd.ExecuteNonQuery()

        End Using
    End Sub
    Public Sub inserpartner(ByVal serverid As Long, ByVal Serverdescription As String, ByVal Serverlink As String, timestamp As String, ByVal Iconurl As String, ByVal Servername As String, ByVal channelid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "INSERT INTO Partner(Serverid,Serverdescription,Serverlink,timestamp,Iconurl,Servername,Channelid) VALUES (@Serverid, @Serverdescription, @Serverlink, @timestamp, @Iconurl, @Servername, @Channelid)"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@Serverdescription", Serverdescription)
            cmd.Parameters.AddWithValue("@Serverlink", Serverlink)
            cmd.Parameters.AddWithValue("@timestamp", timestamp)
            cmd.Parameters.AddWithValue("@Iconurl", Iconurl)
            cmd.Parameters.AddWithValue("@Servername", Servername)
            cmd.Parameters.AddWithValue("@Channelid", channelid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()

        End Using
    End Sub
    Public Sub updatepartner(ByVal serverid As Long, ByVal Serverdescription As String, ByVal Serverlink As String, timestamp As String, ByVal Iconurl As String, ByVal Servername As String, ByVal channelid As Long)

        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "UPDATE Partner SET Serverdescription=@Serverdescription,Serverlink=@Serverlink,timestamp=@timestamp,Iconurl=@Iconurl,Servername=@Servername,Channelid=@Channelid WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@Serverdescription", Serverdescription)
            cmd.Parameters.AddWithValue("@Serverlink", Serverlink)
            cmd.Parameters.AddWithValue("@timestamp", timestamp)
            cmd.Parameters.AddWithValue("@Iconurl", Iconurl)
            cmd.Parameters.AddWithValue("@Servername", Servername)
            cmd.Parameters.AddWithValue("@Channelid", channelid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()


        End Using
    End Sub
    Public Sub updateauctionstatus(ByVal serverid As Long, ByVal status As String, ByVal role As Long)

        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "UPDATE AuctionStatus SET Status=@Status,role=@role WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@Status", status)
            cmd.Parameters.AddWithValue("@role", role)
            sqlconn.Open()
            cmd.ExecuteNonQuery()


        End Using
    End Sub
    Public Sub insertauction(ByVal serverid As Long, ByVal channelid As Long, ByVal msgid As Long, ByVal embedid As Long, ByVal item As String, ByVal desc As String, ByVal payment As String, ByVal startingbid As Integer, ByVal bidinc As Integer, ByVal time As String, ByVal link As String, ByVal snipeguard As Boolean, ByVal openerid As Long, ByVal openerlink As String, ByVal firstbid As Long, ByVal lastbidid As Long, ByVal requester As String, ByVal lastbidname As String)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "INSERT INTO Auctions(Serverid,Channelid,msgid,embedid,item,description,payment,startingbid,bidinc,time,link,snipeguard,openerid,openerlink,firstbid,lastbidid,requestedby,lastbidname) VALUES (@serverid,@channelid,@msgid,@embedid,@item,@description,@payment,@startingbid,@bidinc,@time,@link,@snipeguard,@openerid,@openerlink,@firstbid,@lastbidid,@requestedby,@lastbidname)"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@channelid", channelid)
            cmd.Parameters.AddWithValue("@msgid", msgid)
            cmd.Parameters.AddWithValue("@embedid", embedid)
            cmd.Parameters.AddWithValue("@item", item)
            cmd.Parameters.AddWithValue("@description", desc)
            cmd.Parameters.AddWithValue("@startingbid", startingbid)
            cmd.Parameters.AddWithValue("@bidinc", bidinc)
            cmd.Parameters.AddWithValue("@time", time)
            cmd.Parameters.AddWithValue("@link", link)
            cmd.Parameters.AddWithValue("@snipeguard", snipeguard)
            cmd.Parameters.AddWithValue("@openerid", openerid)
            cmd.Parameters.AddWithValue("@openerlink", openerlink)
            cmd.Parameters.AddWithValue("@firstbid", firstbid)
            cmd.Parameters.AddWithValue("@lastbidid", lastbidid)
            cmd.Parameters.AddWithValue("@requestedby", requester)
            cmd.Parameters.AddWithValue("@lastbidname", lastbidname)
            cmd.Parameters.AddWithValue("@payment", payment)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Function getauctiond(ByVal channelid As Long, ByVal i As Integer)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT startingbid,firstbid,lastbidid,lastbidname,snipeguard,time,payment,item,description,bidinc,requestedby,openerlink,msgid,embedid,openerid,link,lastbidid FROM Auctions WHERE Channelid = @Channelid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@Channelid", channelid)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())
                    Select Case i
                        Case 1
                            Return (reader("startingbid"))
                        Case 2
                            Return (reader("firstbid"))
                        Case 3
                            Return (reader("lastbidname"))
                        Case 4
                            Return (reader("snipeguard"))
                        Case 5
                            Return (reader("time"))
                        Case 6
                            Return (reader("payment"))
                        Case 7
                            Return (reader("item"))
                        Case 8
                            Return (reader("description"))
                        Case 9
                            Return (reader("bidinc"))
                        Case 10
                            Return (reader("requestedby"))
                        Case 11
                            Return (reader("openerlink"))
                        Case 12
                            Return (reader("msgid"))
                        Case 13
                            Return (reader("embedid"))
                        Case 14
                            Return (reader("openerid"))
                        Case 15
                            Return (reader("link"))
                        Case 16
                            Return (reader("lastbidid"))
                    End Select
                End While

            End Using
        End Using
        Return Nothing
    End Function
    Public Sub updateauction(ByVal channelid As Long, ByVal lastbid As Integer, ByVal lastbidname As String, ByVal lastbidid As Long, ByVal time As String)

        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "UPDATE Auctions SET firstbid=@firstbid,lastbidname=@lastbidname,lastbidid=@lastbidid,time=@time WHERE Channelid=@channelid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@channelid", channelid)
            cmd.Parameters.AddWithValue("@firstbid", lastbid)
            cmd.Parameters.AddWithValue("@lastbidname", lastbidname)
            cmd.Parameters.AddWithValue("@lastbidid", lastbidid)
            cmd.Parameters.AddWithValue("@time", time)
            sqlconn.Open()
            cmd.ExecuteNonQuery()




        End Using
    End Sub
    Public Sub insertuserban(ByVal serverid As Long, ByVal userid As Long, ByVal reason As String, ByVal Bannedby As String)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "INSERT INTO Userban(Serverid,Userid,Reason,Bannedby) VALUES (@serverid,@userid,@reason,@Bannedby)"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@userid", userid)
            cmd.Parameters.AddWithValue("@reason", reason)
            cmd.Parameters.AddWithValue("@Bannedby", Bannedby)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub



    Public Sub unbanuser(ByVal userid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "DELETE FROM Userban WHERE Userid = @userid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@userid", userid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Function checkuserbanstatus(ByVal userid As Long, Optional ByVal getasboolean As Boolean = False, Optional getreason As Boolean = False)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT Userid,Serverid,Reason,BannedBy FROM Userban WHERE Userid = @userid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@userid", userid)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                If reader.HasRows Then

                    Dim name As String = Nothing
                    Dim reason As String = Nothing
                    While (reader.Read())
                        name = "Userid: " & reader("Userid") & " Reason: " & reader("Reason") & " Banned by: " & reader("BannedBy")
                        reason = reader("Reason")
                    End While

                    If getasboolean = True Then
                        Return True
                    ElseIf getasboolean = False AndAlso getreason = True Then
                        Return reason
                    ElseIf getasboolean = False AndAlso getreason = False Then
                        Return name
                    End If

                Else

                    Return False
                End If

            End Using
        End Using
        Return Nothing
    End Function
    Public Function getuserbanlist()
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT Userid,Reason FROM Userban"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                Dim name As String = Nothing
                banlist.Clear()
                banlistwithreason.Clear()
                While (reader.Read())
                    banlist.Add(reader("Userid"))
                    banlistwithreason.Add(reader("Userid"), reader("Reason"))

                End While
                Return name
            End Using
        End Using
        Return Nothing
    End Function

    Public Sub insertban(ByVal serverid As Long, ByVal reason As String, ByVal Bannedby As String)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "INSERT INTO Blacklist(Serverid,Reason,Bannedby) VALUES (@serverid,@reason,@Bannedby)"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@reason", reason)
            cmd.Parameters.AddWithValue("@Bannedby", Bannedby)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Function getsetupstatus(ByVal serverid As Long, ByVal i As Integer)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT Is1channel,Logs,Abanasync FROM Setups WHERE Serverid= @serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())

                    Select Case i
                        Case 1
                            Return (reader("Is1channel"))
                        Case 2
                            Return (reader("Logs"))
                        Case 3
                            If (reader("Abanasync")) = "False" Then
                                Return False
                            Else
                                Return True
                            End If

                    End Select



                End While

            End Using
        End Using
        Return Nothing
    End Function
    Public Function Getthememberid(ByVal epicid As String)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT Memberid,Epicid FROM EpicPl WHERE Epicid= @epicid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@epicid", epicid)
            sqlconn.Open()
            ordersexpire.Clear()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())
                    Return reader("Memberid")
                End While

            End Using
        End Using
        Return Nothing
    End Function
    Public Sub insertverification(ByVal userid As Long, ByVal epicid As String)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "INSERT INTO EpicPL(Memberid,Epicid) VALUES (@memberid,@epicid)"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@memberid", userid)
            cmd.Parameters.AddWithValue("@epicid", epicid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Function Gettheepic(ByVal userid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT Memberid,Epicid FROM EpicPl WHERE Memberid= @memberid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@memberid", userid)
            sqlconn.Open()
            ordersexpire.Clear()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())
                    Return reader("Epicid")
                End While

            End Using
        End Using
        Return Nothing
    End Function
    Public Function Checkepicverify(ByVal epicid As String)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM Epicpl WHERE Epicid=@epicid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@epicid", epicid)
            sqlconn.Open()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                If reader.HasRows Then

                    Return True
                Else

                    Return False
                End If

            End Using
        End Using

    End Function
    Public Function Checkmemberverify(ByVal userid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM Epicpl WHERE Memberid=@memberid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@memberid", userid)
            sqlconn.Open()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                If reader.HasRows Then

                    Return True
                Else

                    Return False
                End If

            End Using
        End Using

    End Function
    Public Function gettopmoney(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT Userid,Amount FROM Money WHERE Serverid = @serverid AND Amount != 0 ORDER BY Amount DESC LIMIT 10"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()
            topmoney.Clear()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())
                    topmoney.Add(reader("Userid"), reader("Amount"))
                End While

            End Using
        End Using

        Return Nothing
    End Function
    Public Function getbreeders()
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM Breeders ORDER BY Tame ASC"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            sqlconn.Open()
            paginatedpages.Clear()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())
                    paginatedpages.Add(reader("Tame") & "|" & reader("serverlink"), $"Its brought to you by **{reader("Breedername")}**")
                End While

            End Using
        End Using
        Return Nothing
    End Function
    Public Sub deletebreeder(ByVal userid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "DELETE FROM Breeders WHERE breederid=@breederid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@breederid", userid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Sub insertbreeder(ByVal breederid As Long, ByVal serverlink As String, ByVal tame As String, ByVal breedername As String)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "INSERT INTO Breeders(Breederid,Tame,serverlink,Breedername) VALUES (@breederid,@tame,@serverlink,@breedername)"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@breederid", breederid)
            cmd.Parameters.AddWithValue("@tame", tame)
            cmd.Parameters.AddWithValue("@serverlink", serverlink)
            cmd.Parameters.AddWithValue("@breedername", breedername)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Function Checkbreederexist(ByVal userid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM Breeders WHERE Breederid=@breederid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@breederid", userid)
            sqlconn.Open()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                If reader.HasRows Then

                    Return True
                Else

                    Return False
                End If

            End Using
        End Using

    End Function
    Public Function checksetup(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM Setups WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                If reader.HasRows Then

                    Return True
                Else

                    Return False
                End If

            End Using
        End Using

    End Function
    Public Sub insertsetup(ByVal serverid As Long, ByVal the1channel As Long, ByVal logs As Long, ByVal abanasync As String)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "INSERT INTO Setups(Serverid,Is1channel,Logs,Abanasync) VALUES (@serverid,@is1channel,@logs,@aban)"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@is1channel", the1channel)
            cmd.Parameters.AddWithValue("@logs", logs)
            cmd.Parameters.AddWithValue("@aban", abanasync)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Sub updatesetup(ByVal serverid As Long, ByVal the1channel As Long, ByVal logs As Long, ByVal abanasync As String)

        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "UPDATE Setups SET Serverid=@serverid,Is1channel=@Is1channel,Logs=@Logs,Abanasync=@Abanasync WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@Is1channel", the1channel)
            cmd.Parameters.AddWithValue("@Logs", logs)
            cmd.Parameters.AddWithValue("@Abanasync", abanasync)
            sqlconn.Open()
            cmd.ExecuteNonQuery()


        End Using
    End Sub
    Public Sub insertauctionstatus(ByVal serverid As Long, ByVal status As String, ByVal role As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "INSERT INTO AuctionStatus(Serverid,Status,role) VALUES (@serverid,@status,@role)"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@status", status)
            cmd.Parameters.AddWithValue("@role", role)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub removeallowedchans(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "DELETE FROM AuctionChannels WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Sub removeexallowedchannel(ByVal serverid As Long, ByVal channelid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "DELETE FROM AuctionChannels WHERE Serverid=@serverid AND Channelid=@channelid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@channelid", channelid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Function getallowedchans(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT Serverid,Channelid FROM AuctionChannels WHERE Serverid= @serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()
            channelidssss.Clear()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())
                    channelidssss.Add(reader("Channelid"))
                End While

            End Using
        End Using
        Return Nothing
    End Function
    Public Function getorderexpire(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT orderpostid,submittime FROM OrdersList WHERE Serverid= @serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()
            ordersexpire.Clear()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())
                    ordersexpire.Add(reader("orderpostid"), reader("submittime"))
                End While

            End Using
        End Using
        Return Nothing
    End Function

    Public Sub insertallowedauctionchan(ByVal serverid As Long, ByVal channelid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "INSERT INTO AuctionChannels(Serverid,Channelid) VALUES (@serverid,@channelid)"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@channelid", channelid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Function checkallowedchan(ByVal serverid As Long, ByVal channelid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM AuctionChannels WHERE Serverid=@serverid AND Channelid = @channelid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@channelid", channelid)
            sqlconn.Open()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                If reader.HasRows Then

                    Return True
                Else

                    Return False
                End If

            End Using
        End Using

    End Function
    Public Sub delsbsr(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "DELETE FROM Paid WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Sub updatepaid(ByVal serverid As Long, ByVal amount As Integer, ByVal stdate As Date)

        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "UPDATE Paid SET Serverid=@serverid,Amount=@amount,Starteddate=@starteddate WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@amount", amount)
            cmd.Parameters.AddWithValue("@starteddate", stdate)
            sqlconn.Open()
            cmd.ExecuteNonQuery()


        End Using
    End Sub
    Public Sub insertpaid(ByVal serverid As Long, ByVal amount As Integer, ByVal stdate As Date)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "INSERT INTO Paid(Serverid,Amount,Starteddate) VALUES (@serverid,@amount,@starteddate)"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@amount", amount)
            cmd.Parameters.AddWithValue("@starteddate", stdate)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Function getpaid(ByVal serverid As Long, ByVal i As Integer)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT Amount,Starteddate FROM Paid WHERE Serverid= @serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())

                    Select Case i
                        Case 1
                            Return (reader("Amount"))
                        Case 2
                            Return (reader("Starteddate"))
                    End Select



                End While

            End Using
        End Using
        Return Nothing
    End Function
    Public Function checkpaid(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM Paid WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                If reader.HasRows Then

                    Return True
                Else

                    Return False
                End If

            End Using
        End Using

    End Function
    Public Sub removeauctionstatus(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "DELETE FROM AuctionStatus WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Function checkauctionstatus(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM AuctionStatus WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                If reader.HasRows Then

                    Return True
                Else

                    Return False
                End If

            End Using
        End Using

    End Function
    Public Function checkauctionexist(ByVal serverid As Long, ByVal channelid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM Auctions WHERE Serverid=@serverid AND Channelid=@channelid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@channelid", channelid)
            sqlconn.Open()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                If reader.HasRows Then

                    Return True
                Else

                    Return False
                End If

            End Using
        End Using

    End Function
    Public Function getauctionstatus(ByVal serverid As Long, ByVal i As Integer)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT Status,role FROM AuctionStatus WHERE Serverid= @serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())
                    Select Case i
                        Case 1
                            Return (reader("Status"))
                        Case 2
                            Return (reader("role"))

                    End Select
                End While

            End Using
        End Using
        Return Nothing
    End Function
    Public Sub updateorder(ByVal serverid As Long, ByVal postchannel As Long, ByVal submitchannel As Long, ByVal rolemention As Long, ByVal Servername As String, ByVal Track As Boolean, ByVal channelid As Long)

        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "UPDATE Orders SET Postchannel=@Postchannel,Submitchannel=@Submitchannel,Rolemention=@Rolemention,Servername=@Servername,Track=@Track,Channelid=@Channelid WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@Postchannel", postchannel)
            cmd.Parameters.AddWithValue("@Submitchannel", submitchannel)
            cmd.Parameters.AddWithValue("@Rolemention", rolemention)
            cmd.Parameters.AddWithValue("@Servername", Servername)
            cmd.Parameters.AddWithValue("@Track", Track.ToString)
            cmd.Parameters.AddWithValue("@Channelid", channelid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()


        End Using
    End Sub
    Public Function getmoney(ByVal Serverid As Long, ByVal Userid As Long, ByVal int As Integer)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT Amount,LastCheck FROM Money WHERE Serverid = @serverid AND Userid = @userid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", Serverid)
            cmd.Parameters.AddWithValue("@userid", Userid)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader

                While (reader.Read())
                    Select Case int
                        Case 1
                            Return (reader("Amount"))
                        Case 2
                            Return (reader("LastCheck"))
                    End Select
                End While

            End Using
        End Using
        Return Nothing
    End Function
    Public Sub updatemoney(ByVal serverid As Long, ByVal userid As Long, ByVal amount As Integer, ByVal lastcheck As String)

        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "UPDATE Money SET Amount=@amount,LastCheck=@lastcheck WHERE Serverid=@serverid AND Userid = @userid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@amount", amount)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@userid", userid)
            cmd.Parameters.AddWithValue("@lastcheck", lastcheck)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Sub insertmoneystatus(ByVal serverid As Long, ByVal daily As Integer, ByVal points As Integer)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "INSERT INTO MoneyStatus(Serverid,Daily,Points) VALUES (@serverid,@daily,@points)"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@daily", daily)
            cmd.Parameters.AddWithValue("@points", points)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Sub insertwallet(ByVal serverid As Long, ByVal userid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "INSERT INTO Money(Serverid,Userid) VALUES (@serverid,@userid)"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@userid", userid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Sub resetwallets(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "DELETE FROM Money WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Sub removemoneystatus(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "DELETE FROM MoneyStatus WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Sub removecurrencystatus(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "DELETE FROM CurrencyRatesStatus WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Sub insertcurrencystatus(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "INSERT INTO CurrencyRatesStatus(Serverid) VALUES (@serverid)"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Function checkcurrency(ByVal id As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM CurrencyRatesStatus WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", id)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                If reader.HasRows Then
                    Return True
                Else
                    Return False
                End If
            End Using

        End Using
    End Function
    Public Sub removeserverstatus(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "DELETE FROM ServerStatus WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Sub insertserverstats(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "INSERT INTO ServerStatus(Serverid) VALUES (@serverid)"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Function checksstats(ByVal id As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM ServerStatus WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", id)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                If reader.HasRows Then
                    Return True
                Else
                    Return False
                End If
            End Using

        End Using
    End Function

    Public Function getrates(ByVal i As Integer)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT Money,Tek,Ingots,Poly,Crystal,CP FROM CurrencyRates"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())
                    Select Case i
                        Case 1
                            Return (reader("Money"))
                        Case 2
                            Return (reader("Tek"))
                        Case 3
                            Return (reader("Ingots"))
                        Case 4
                            Return (reader("Poly"))
                        Case 5
                            Return (reader("Crystal"))
                        Case 6
                            Return (reader("CP"))
                    End Select



                End While

            End Using
        End Using

        Return Nothing
    End Function

    Public Function getglchat(ByVal serverid As Long, ByVal i As Integer)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT Channelid FROM Globalchat WHERE Serverid= @serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())
                    Select Case i
                        Case 1
                            Return (reader("Channelid"))
                    End Select



                End While

            End Using
        End Using

        Return Nothing
    End Function
    Public Sub insertglobalchat(ByVal serverid As Long, ByVal channelid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "INSERT INTO Globalchat(Serverid,Channelid) VALUES (@serverid,@channelid)"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@channelid", channelid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Sub updateglobalchat(ByVal serverid As Long, ByVal channelid As Long)

        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "UPDATE Globalchat SET Channelid = @channelid WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@channelid", channelid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()


        End Using
    End Sub
    Public Sub removeglobalchat(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "DELETE FROM Globalchat WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Function checkglobalchat(ByVal id As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM Globalchat WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", id)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                If reader.HasRows Then
                    Return True
                Else
                    Return False
                End If
            End Using

        End Using
    End Function
    Public Function checkmoneystatus(ByVal id As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM MoneyStatus WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", id)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                If reader.HasRows Then
                    Return True
                Else
                    Return False
                End If
            End Using

        End Using
    End Function
    Public Function checkdaily(ByVal id As Long, ByVal int As Integer)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT Serverid,Daily,Points FROM MoneyStatus WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", id)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())
                    Select Case int
                        Case 1
                            If (reader("Daily")) = 1 Then
                                Return True
                            ElseIf (reader("Daily")) = 0 Then
                                Return False
                            End If
                        Case 2
                            Return (reader("Points"))

                    End Select

                End While

            End Using

        End Using
    End Function
    Public Function ifwalletexist(ByVal userid As Long, ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM Money WHERE Userid=@userid AND Serverid = @serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@userid", userid)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                If reader.HasRows Then
                    Return True
                Else
                    Return False
                End If
            End Using

        End Using
    End Function
    Public Function checkblacklist(ByVal id As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM Blacklist WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", id)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                If reader.HasRows Then
                    Return True
                Else
                    Return False
                End If
            End Using

        End Using
    End Function
    Public Function checkmuted(ByVal userid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM Glchatmuted WHERE Userid=@userid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@userid", userid)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                If reader.HasRows Then
                    Return True
                Else
                    Return False
                End If
            End Using

        End Using
    End Function
    Public Sub removemute(ByVal userid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "DELETE FROM Glchatmuted WHERE Userid=@userid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@userid", userid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Sub insertmute(ByVal userid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "INSERT INTO Glchatmuted(Userid) VALUES (@userid)"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@userid", userid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Sub removeexpiredorder(ByVal serverid As Long, ByVal orderid As Long)

        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "DELETE FROM OrdersList WHERE Serverid=@serverid AND orderpostid = @orderpostid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@orderpostid", orderid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Function checkorder(ByVal id As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM Orders WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", id)
            sqlconn.Open()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                If reader.HasRows Then

                    Return True
                Else

                    Return False
                End If

            End Using
        End Using

    End Function
    Public Function gettemplate(ByVal serverid As Long, ByVal name As String)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM OrderTemplates WHERE Serverid = @serverid AND Name = @name"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@name", name)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())
                    Return (reader("Template"))
                End While

            End Using
        End Using
        Return Nothing
    End Function
    Public Sub removeodtemplate(ByVal serverid As Long, ByVal name As String)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "DELETE FROM OrderTemplates WHERE Serverid = @serverid AND Name = @name"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@name", name)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Function getalltemplates(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM OrderTemplates WHERE Serverid = @serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                Dim k As Integer = 0
                Dim ko As String = Nothing
                While (reader.Read())
                    k += 1
                    ko += $"{k} - " & reader("Name") & System.Environment.NewLine

                End While
                Return ko
            End Using
        End Using
        Return Nothing
    End Function
    Public Sub insertordertemplate(ByVal serverid As Long, ByVal template As String, ByVal name As String)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "INSERT INTO OrderTemplates(Serverid,Template,Name) VALUES (@serverid,@template,@name)"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@template", template)
            cmd.Parameters.AddWithValue("@name", name)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Function checkordertemplatescount(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT COUNT(ID) FROM OrderTemplates where Serverid = @serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())
                    Return (reader("COUNT(ID)"))
                End While

            End Using
        End Using
        Return Nothing
    End Function
    Public Function checkordertemplateexist(ByVal serverid As Long, ByVal name As String)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM OrderTemplates where Serverid = @serverid AND Name = @name"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@name", name)
            sqlconn.Open()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                If reader.HasRows Then
                    Return True
                Else
                    Return False
                End If

            End Using
        End Using
        Return Nothing
    End Function
    Public Function checkifpartner(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT * FROM Partner WHERE Serverid=@serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()
            Using reader As SQLiteDataReader = cmd.ExecuteReader
                If reader.HasRows Then

                    Return True
                Else

                    Return False
                End If

            End Using
        End Using

    End Function
    Public Function getauctionservid(ByVal channelid As Long, ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT ID FROM Auctions WHERE Channelid = @channelid AND Serverid = @serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@channelid", channelid)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())
                    Return (reader("ID"))
                End While

            End Using
        End Using
        Return Nothing
    End Function
    Public Function getauction(ByVal serverid As Long, ByVal ID As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT Channelid FROM Auctions WHERE Serverid = @serverid AND ID=@id"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@id", ID)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())
                    Return (reader("Channelid"))
                End While

            End Using
        End Using
        Return Nothing
    End Function
    Public Function getblacklist()
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT Serverid,Reason,Bannedby FROM Blacklist"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                Dim name As String
                Dim k As Integer
                While (reader.Read())
                    k += 1
                    name += k & "- " & "Server ID: " & (reader("Serverid")) & " - Reason: " & (reader("Reason")) & " - Bannedby: " & (reader("Bannedby")) & System.Environment.NewLine
                End While
                Return name
            End Using
        End Using
        Return Nothing
    End Function
    Public Function getreports()
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT Reportby,Reportedserver,Reason FROM Reports"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                Dim name As String
                Dim k As Integer
                While (reader.Read())
                    k += 1
                    name += k & "- " & "Report by: " & (reader("Reportby")) & " - Reported Server: " & (reader("Reportedserver")) & " - Reason: " & (reader("Reason")) & System.Environment.NewLine
                End While
                Return name
            End Using
        End Using
        Return Nothing
    End Function
    Public Sub unbanserver(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "DELETE FROM Blacklist WHERE Serverid = @serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Sub removeblacklist()
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "DELETE FROM Blacklist"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Sub removereports()
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "DELETE FROM Reports"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            sqlconn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Function getpartnersettings(ByVal serverid As Long, ByVal i As Integer)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT Servername,Serverdescription,Serverlink,timestamp,Iconurl,Channelid FROM Partner WHERE Serverid = @serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())
                    Select Case i
                        Case 1
                            Return (reader("Servername"))
                        Case 2
                            Return (reader("Serverdescription"))
                        Case 3
                            Return (reader("Serverlink"))
                        Case 4
                            Return (reader("timestamp"))
                        Case 5
                            Return (reader("Iconurl"))
                        Case 6
                            Return (reader("Channelid"))
                    End Select



                End While

            End Using
        End Using

        Return Nothing
    End Function

    Public Function getordersettings(ByVal serverid As Long, ByVal i As Integer)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "SELECT Postchannel,Submitchannel,Rolemention,Channelid FROM Orders WHERE Serverid= @serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()

            Using reader As SQLiteDataReader = cmd.ExecuteReader
                While (reader.Read())
                    Select Case i
                        Case 1
                            Return (reader("Postchannel"))
                        Case 2
                            Return (reader("Submitchannel"))
                        Case 3
                            Return (reader("Rolemention"))
                        Case 4
                            Return (reader("Channelid"))
                    End Select



                End While

            End Using
        End Using

        Return Nothing
    End Function
    Public Sub removeauction(ByVal serverid As Long, ByVal channelid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "DELETE FROM Auctions WHERE Serverid = @serverid AND Channelid = @channelid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            cmd.Parameters.AddWithValue("@channelid", channelid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()

        End Using
    End Sub
    Public Sub removeserver(ByVal serverid As Long)
        Using sqlconn As New SQLiteConnection(connectionstr)
            Dim insert As String = "DELETE FROM Orders WHERE Serverid = @serverid"
            Dim cmd As New SQLiteCommand(insert, sqlconn)
            cmd.Parameters.AddWithValue("@serverid", serverid)
            sqlconn.Open()
            cmd.ExecuteNonQuery()

        End Using
    End Sub
    Private Function duplicatedatabase(ByVal fullpath As String) As Boolean
        Return System.IO.File.Exists(fullpath)
    End Function
End Class
