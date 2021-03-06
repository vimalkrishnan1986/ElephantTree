USE [master]
GO
/****** Object:  Database [KannanTyres]    Script Date: 06-Feb-19 2:44:10 AM ******/
CREATE DATABASE [KannanTyres] 
go
USE [KannanTyres]
GO
/****** Object:  Table [dbo].[Barcode Master]    Script Date: 06-Feb-19 2:44:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Barcode Master](
	[Barcode Number] [nvarchar](50) NOT NULL,
	[Billing Number] [nvarchar](max) NULL,
	[Billing Token number] [nvarchar](250) NULL,
	[Date] [date] NOT NULL,
	[Marchent Token number] [nvarchar](250) NULL,
	[Image] [image] NOT NULL,
 CONSTRAINT [PK_Barcode Master] PRIMARY KEY CLUSTERED 
(
	[Barcode Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Billing Details]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Billing Details](
	[Billing details number] [int] IDENTITY(1,1) NOT NULL,
	[Billing Token number] [nvarchar](250) NULL,
	[Billing number] [nvarchar](max) NULL,
	[Date] [date] NOT NULL,
	[Product Token] [nvarchar](250) NULL,
	[Pieces] [int] NOT NULL,
	[Amount] [numeric](18, 2) NOT NULL,
	[Taxable amount] [numeric](18, 2) NOT NULL,
	[Tax] [numeric](18, 2) NOT NULL,
	[Discount percent] [numeric](18, 2) NOT NULL,
	[Discount] [numeric](18, 2) NOT NULL,
	[Sub Total] [numeric](18, 2) NOT NULL,
	[Product name] [nvarchar](250) NULL,
 CONSTRAINT [PK_Billing Details] PRIMARY KEY CLUSTERED 
(
	[Billing details number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Billing Master]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Billing Master](
	[Token Number] [nvarchar](250) NOT NULL,
	[Billing Number] [nvarchar](max) NULL,
	[Date] [date] NOT NULL,
	[Marchent Token number] [nvarchar](250) NULL,
	[Customer Token number] [nvarchar](250) NULL,
	[Tax token] [nvarchar](250) NULL,
	[Total tax] [numeric](18, 2) NOT NULL,
	[Rate including tax] [numeric](18, 2) NOT NULL,
	[Discount percent] [numeric](18, 2) NOT NULL,
	[Total discount] [numeric](18, 2) NOT NULL,
	[Total amount] [numeric](18, 2) NOT NULL,
	[CGST] [numeric](18, 2) NOT NULL,
	[SGST] [numeric](18, 2) NOT NULL,
	[Narration] [nvarchar](max) NULL,
	[Barcode Number] [nvarchar](50) NULL,
	[Barcode photo] [image] NOT NULL,
	[Payment Statement] [nvarchar](50) NULL,
 CONSTRAINT [PK_Billing Master] PRIMARY KEY CLUSTERED 
(
	[Token Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customer]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Token number] [nvarchar](250) NOT NULL,
	[Customer Name] [nvarchar](250) NULL,
	[Email] [nvarchar](250) NULL,
	[Password] [nvarchar](250) NULL,
	[Phone number] [nvarchar](50) NULL,
	[Alternate phone number] [nvarchar](50) NULL,
	[State] [int] NULL,
	[State Name] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[Second Address] [nvarchar](max) NULL,
	[Pan number] [nvarchar](50) NULL,
	[Credit Card number] [nvarchar](50) NULL,
	[Credit limit] [float] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Marchent Token number] [nvarchar](250) NULL,
	[Applied date] [date] NOT NULL,
	[Approve date] [date] NOT NULL,
	[New customer] [bit] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Token number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customer shipping address]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer shipping address](
	[Customer token number] [nvarchar](250) NOT NULL,
	[First name] [nvarchar](250) NULL,
	[Last name] [nvarchar](250) NULL,
	[Address Line1] [nvarchar](250) NULL,
	[Address Line2] [nvarchar](250) NULL,
	[Town/City] [nvarchar](250) NULL,
	[State] [nvarchar](50) NULL,
	[Pin code] [nvarchar](50) NULL,
	[Phone number] [nvarchar](50) NULL,
	[Email] [nvarchar](250) NULL,
	[IsUser] [bit] NOT NULL,
 CONSTRAINT [PK_Customer shipping address] PRIMARY KEY CLUSTERED 
(
	[Customer token number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Dealer]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dealer](
	[Token number] [nvarchar](250) NOT NULL,
	[Dealer code] [nvarchar](50) NULL,
	[Name] [nvarchar](250) NULL,
	[Email] [nvarchar](250) NULL,
	[Address] [nvarchar](max) NULL,
	[Phone number] [nvarchar](50) NULL,
	[State] [int] NULL,
	[State Name] [nvarchar](50) NULL,
	[Isactive] [bit] NOT NULL,
	[Marchent Token number] [nvarchar](250) NULL,
 CONSTRAINT [PK_Dealer] PRIMARY KEY CLUSTERED 
(
	[Token number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employee]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Token number] [nvarchar](50) NOT NULL,
	[Employee Id] [nvarchar](50) NULL,
	[Employee name] [nvarchar](100) NULL,
	[Designation] [nvarchar](250) NULL,
	[Joining date] [date] NOT NULL,
	[Contact number] [nvarchar](50) NULL,
	[Email id] [nvarchar](250) NULL,
	[Salary] [decimal](18, 2) NOT NULL,
	[Leaving date] [date] NOT NULL,
	[login required] [bit] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Token number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Item Tube]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item Tube](
	[Token number] [nvarchar](50) NOT NULL,
	[Item Id] [nvarchar](50) NULL,
	[Tube size] [nvarchar](50) NULL,
	[Company name] [nvarchar](50) NULL,
	[Vehicle type] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_Item Tube] PRIMARY KEY CLUSTERED 
(
	[Token number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Item Tyre]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item Tyre](
	[Token number] [nvarchar](250) NOT NULL,
	[Item Id] [nvarchar](50) NULL,
	[Tyre make] [nvarchar](50) NULL,
	[Tyre type] [nvarchar](50) NULL,
	[Tyre feel] [nvarchar](50) NULL,
	[Company name] [nvarchar](50) NULL,
	[Tyre size] [nvarchar](50) NULL,
	[Vehicle type] [nvarchar](50) NULL,
	[Description] [nvarchar](250) NULL,
 CONSTRAINT [PK_Item Tyre] PRIMARY KEY CLUSTERED 
(
	[Token number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Manager]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manager](
	[Token number] [nvarchar](50) NOT NULL,
	[Manager name] [nvarchar](max) NULL,
	[Email Id] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Mobile] [nvarchar](50) NULL,
	[State Code] [int] NULL,
	[Pan Number] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[Verification code] [nvarchar](max) NULL,
	[State Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Manager] PRIMARY KEY CLUSTERED 
(
	[Token number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Marchent Account]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Marchent Account](
	[Token number] [nvarchar](50) NOT NULL,
	[Marchent name] [nvarchar](max) NULL,
	[Email Id] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Mobile] [nvarchar](50) NULL,
	[Telephone No] [nvarchar](50) NULL,
	[GSTIN Number] [nvarchar](max) NULL,
	[CIN Number] [nvarchar](max) NULL,
	[UIN Number] [nvarchar](max) NULL,
	[State Code] [int] NULL,
	[Pan Number] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[Verification code] [nvarchar](max) NULL,
	[State Name] [nvarchar](50) NULL,
	[License] [nvarchar](max) NULL,
 CONSTRAINT [PK_Marchent Account] PRIMARY KEY CLUSTERED 
(
	[Token number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Marchent account payment details]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Marchent account payment details](
	[GLCODE] [nvarchar](250) NOT NULL,
	[Account name] [nvarchar](max) NULL,
	[Card number] [nvarchar](50) NULL,
	[Marchent token] [nvarchar](250) NULL,
 CONSTRAINT [PK_Marchent account payment details] PRIMARY KEY CLUSTERED 
(
	[GLCODE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Other Product]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Other Product](
	[Token number] [nvarchar](250) NOT NULL,
	[Product name] [nvarchar](250) NULL,
	[Product type] [nvarchar](250) NULL,
	[Description] [nvarchar](300) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Token number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Placed Order]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Placed Order](
	[Token number] [nvarchar](250) NOT NULL,
	[Item token] [nvarchar](250) NULL,
	[Pieces] [int] NOT NULL,
	[Orderplaced] [bit] NOT NULL,
	[Customer token] [nvarchar](250) NULL,
	[IsUser] [bit] NOT NULL,
	[Order Date] [date] NOT NULL,
	[Ispaid] [bit] NOT NULL,
	[Approve Date] [date] NOT NULL,
 CONSTRAINT [PK_Placed Order] PRIMARY KEY CLUSTERED 
(
	[Token number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Token Number] [nvarchar](250) NOT NULL,
	[Product Code] [nvarchar](50) NULL,
	[Product name] [nvarchar](250) NULL,
	[Description] [nvarchar](max) NULL,
	[HSN/SAC Code] [nvarchar](max) NULL,
	[Tax rate] [numeric](18, 2) NOT NULL,
	[GL CODE] [nvarchar](250) NULL,
	[IsActive] [bit] NOT NULL,
	[Marchent Token number] [nvarchar](250) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Token Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products For Sale]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products For Sale](
	[Token Number] [nvarchar](250) NOT NULL,
	[Product Token] [nvarchar](250) NOT NULL,
	[Product name] [nvarchar](250) NULL,
	[Tyre Size] [nvarchar](250) NULL,
	[Supplier token] [nvarchar](250) NULL,
	[Supplier name] [nvarchar](250) NOT NULL,
	[Date] [date] NOT NULL,
	[Purchase Price] [numeric](18, 2) NOT NULL,
	[CGST] [numeric](18, 2) NOT NULL,
	[SGST] [numeric](18, 2) NOT NULL,
	[Pieces] [int] NOT NULL,
	[Selling Price] [numeric](18, 2) NOT NULL,
	[Amout after tax] [numeric](18, 2) NOT NULL,
	[Total] [numeric](18, 2) NOT NULL,
	[Approve date] [date] NOT NULL,
	[Approve] [bit] NOT NULL,
	[Administrator Token number] [nvarchar](250) NULL,
	[Administrator name] [nvarchar](250) NULL,
	[Delivery contact number] [bigint] NOT NULL,
	[Delivery address] [nvarchar](300) NULL,
	[Item tyre Id] [nvarchar](250) NULL,
	[Purchase number] [nvarchar](250) NULL,
 CONSTRAINT [PK_Products For Sale] PRIMARY KEY CLUSTERED 
(
	[Token Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Proprietor]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proprietor](
	[Token number] [nvarchar](50) NOT NULL,
	[Proprietor name] [nvarchar](max) NULL,
	[Email Id] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Mobile] [nvarchar](50) NULL,
	[State Code] [int] NULL,
	[Pan Number] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[Verification code] [nvarchar](max) NULL,
	[State Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Proprietor] PRIMARY KEY CLUSTERED 
(
	[Token number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Purchase details]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase details](
	[Purchase details number] [int] IDENTITY(1,1) NOT NULL,
	[Purchase Token number] [nvarchar](250) NULL,
	[Purcahse number] [nvarchar](max) NULL,
	[Date] [datetime] NOT NULL,
	[Product Token] [nvarchar](250) NULL,
	[Product name] [nvarchar](250) NULL,
	[Pieces] [int] NOT NULL,
	[Quantity] [numeric](18, 3) NOT NULL,
	[Amount] [numeric](18, 2) NOT NULL,
	[Taxable amount] [numeric](18, 2) NOT NULL,
	[Tax Token] [nvarchar](250) NULL,
	[Tax] [numeric](18, 2) NOT NULL,
	[Discount percent] [numeric](18, 2) NOT NULL,
	[Discount] [numeric](18, 2) NOT NULL,
	[Sub Total] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_Purchase details] PRIMARY KEY CLUSTERED 
(
	[Purchase details number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Purchase Invoice]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase Invoice](
	[Token number] [nvarchar](250) NOT NULL,
	[Purchase invoice number] [nvarchar](250) NULL,
	[Date] [date] NOT NULL,
	[Stock entry token] [nvarchar](250) NULL,
 CONSTRAINT [PK_Purchase Invoice] PRIMARY KEY CLUSTERED 
(
	[Token number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Purchase Master]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase Master](
	[Token Number] [nvarchar](250) NOT NULL,
	[Purchase Number] [nvarchar](max) NULL,
	[Date] [datetime] NOT NULL,
	[Marchent Token number] [nvarchar](250) NULL,
	[Dealer Token number] [nvarchar](250) NULL,
	[Tax token] [nvarchar](250) NULL,
	[Total tax] [numeric](18, 2) NOT NULL,
	[Rate including tax] [numeric](18, 2) NOT NULL,
	[Discount percent] [numeric](18, 2) NOT NULL,
	[Total discount] [numeric](18, 2) NOT NULL,
	[Total amount] [numeric](18, 2) NOT NULL,
	[CGST] [numeric](18, 2) NOT NULL,
	[SGST] [numeric](18, 2) NOT NULL,
	[IGST] [numeric](18, 2) NOT NULL,
	[UTGST] [numeric](18, 2) NOT NULL,
	[Narration] [nvarchar](max) NULL,
 CONSTRAINT [PK_Purchase Master] PRIMARY KEY CLUSTERED 
(
	[Token Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[State]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[State Code] [int] NOT NULL,
	[Name] [nvarchar](250) NULL,
	[State Identity] [nvarchar](50) NULL,
	[CGST] [bit] NOT NULL,
	[SGST] [bit] NOT NULL,
	[UTGST] [bit] NOT NULL,
	[IGST] [bit] NOT NULL,
	[Marchent Token number] [nvarchar](250) NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[State Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Stock]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[Stock Id] [int] IDENTITY(1,1) NOT NULL,
	[Purchase Token number] [nvarchar](250) NULL,
	[Purcahse number] [nvarchar](max) NULL,
	[Date] [date] NOT NULL,
	[Remodify Date] [date] NOT NULL,
	[Product Token] [nvarchar](250) NULL,
	[Remodify pcs] [int] NOT NULL,
	[Pieces] [int] NOT NULL,
	[CGST] [numeric](18, 2) NOT NULL,
	[SGST] [numeric](18, 2) NOT NULL,
	[Product name] [nvarchar](250) NULL,
	[Marchent Token number] [nvarchar](250) NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[Stock Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Stockout]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stockout](
	[Stock out Id] [int] IDENTITY(1,1) NOT NULL,
	[Billing Token number] [nvarchar](250) NULL,
	[Billing number] [nvarchar](max) NULL,
	[Date] [date] NOT NULL,
	[Product Token] [nvarchar](250) NULL,
	[Pieces] [int] NOT NULL,
	[CGST] [numeric](18, 2) NOT NULL,
	[SGST] [numeric](18, 2) NOT NULL,
	[Sub Total] [numeric](18, 2) NOT NULL,
	[Marchent Token number] [nvarchar](250) NULL,
 CONSTRAINT [PK_Stockout] PRIMARY KEY CLUSTERED 
(
	[Stock out Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tax Group]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tax Group](
	[Tax Token] [nvarchar](250) NOT NULL,
	[Tax Name] [nvarchar](250) NULL,
	[Tax Rate] [float] NOT NULL,
	[GL CODE] [nvarchar](250) NULL,
	[IsActive] [bit] NOT NULL,
	[Marchent Token number] [nvarchar](250) NULL,
 CONSTRAINT [PK_Tax Groups] PRIMARY KEY CLUSTERED 
(
	[Tax Token] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Temp_placedorder]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Temp_placedorder](
	[Token number] [nvarchar](250) NOT NULL,
	[Item token] [nvarchar](250) NULL,
	[Pieces] [int] NULL,
	[Customer token] [nvarchar](250) NULL,
	[IsUser] [bit] NOT NULL,
	[Order Date] [date] NOT NULL,
 CONSTRAINT [PK_Temp_placedorder] PRIMARY KEY CLUSTERED 
(
	[Token number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Transaction amount details]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction amount details](
	[Token Number] [nvarchar](250) NOT NULL,
	[Marchent Token] [nvarchar](250) NULL,
	[Purchase Token] [nvarchar](250) NULL,
	[Purchase number] [nvarchar](max) NULL,
	[Sale invoice token] [nvarchar](250) NULL,
	[Sale invoice number] [nvarchar](max) NULL,
	[GL CODE] [nvarchar](250) NULL,
	[Amount] [numeric](18, 2) NOT NULL,
	[Card number] [nvarchar](50) NULL,
	[Cheque number] [nvarchar](50) NULL,
 CONSTRAINT [PK_Transaction amount details] PRIMARY KEY CLUSTERED 
(
	[Token Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tyre size]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tyre size](
	[Token number] [nvarchar](50) NOT NULL,
	[Tyre size] [nvarchar](50) NULL,
	[With tube] [bit] NOT NULL,
 CONSTRAINT [PK_Tyre size] PRIMARY KEY CLUSTERED 
(
	[Token number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Token number] [nvarchar](250) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Email] [nvarchar](250) NULL,
	[Password] [nvarchar](250) NULL,
	[Phone number] [nvarchar](50) NULL,
	[Alternate phone number] [nvarchar](50) NULL,
	[State] [int] NULL,
	[State Name] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[Second Address] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[Marchent Token number] [nvarchar](250) NULL,
	[Applied date] [date] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Token number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Vehicle]    Script Date: 06-Feb-19 2:44:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicle](
	[Token number] [nvarchar](50) NOT NULL,
	[Vehicle type] [nvarchar](250) NULL,
	[Vehicle make] [nvarchar](250) NULL,
 CONSTRAINT [PK_Vehicle] PRIMARY KEY CLUSTERED 
(
	[Token number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Barcode Master]  WITH CHECK ADD  CONSTRAINT [FK_Barcode Master_Billing Master] FOREIGN KEY([Billing Token number])
REFERENCES [dbo].[Billing Master] ([Token Number])
GO
ALTER TABLE [dbo].[Barcode Master] CHECK CONSTRAINT [FK_Barcode Master_Billing Master]
GO
ALTER TABLE [dbo].[Billing Details]  WITH CHECK ADD  CONSTRAINT [FK_Billing Details_Billing Details] FOREIGN KEY([Billing Token number])
REFERENCES [dbo].[Billing Master] ([Token Number])
GO
ALTER TABLE [dbo].[Billing Details] CHECK CONSTRAINT [FK_Billing Details_Billing Details]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_State] FOREIGN KEY([State])
REFERENCES [dbo].[State] ([State Code])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_State]
GO
ALTER TABLE [dbo].[Dealer]  WITH CHECK ADD  CONSTRAINT [FK_Dealer_State] FOREIGN KEY([State])
REFERENCES [dbo].[State] ([State Code])
GO
ALTER TABLE [dbo].[Dealer] CHECK CONSTRAINT [FK_Dealer_State]
GO
ALTER TABLE [dbo].[Marchent Account]  WITH CHECK ADD  CONSTRAINT [FK_Marchent Account_State] FOREIGN KEY([State Code])
REFERENCES [dbo].[State] ([State Code])
GO
ALTER TABLE [dbo].[Marchent Account] CHECK CONSTRAINT [FK_Marchent Account_State]
GO
ALTER TABLE [dbo].[Products For Sale]  WITH CHECK ADD  CONSTRAINT [FK_Products For Sale_Products] FOREIGN KEY([Product Token])
REFERENCES [dbo].[Products] ([Token Number])
GO
ALTER TABLE [dbo].[Products For Sale] CHECK CONSTRAINT [FK_Products For Sale_Products]
GO
ALTER TABLE [dbo].[Purchase details]  WITH CHECK ADD  CONSTRAINT [FK_Purchase details_Purchase Master] FOREIGN KEY([Purchase Token number])
REFERENCES [dbo].[Purchase Master] ([Token Number])
GO
ALTER TABLE [dbo].[Purchase details] CHECK CONSTRAINT [FK_Purchase details_Purchase Master]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Purchase Master] FOREIGN KEY([Purchase Token number])
REFERENCES [dbo].[Purchase Master] ([Token Number])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Purchase Master]
GO
ALTER TABLE [dbo].[Stockout]  WITH CHECK ADD  CONSTRAINT [FK_Stockout_Billing Master] FOREIGN KEY([Billing Token number])
REFERENCES [dbo].[Billing Master] ([Token Number])
GO
ALTER TABLE [dbo].[Stockout] CHECK CONSTRAINT [FK_Stockout_Billing Master]
GO
USE [master]
GO
ALTER DATABASE [KannanTyres] SET  READ_WRITE 
GO
