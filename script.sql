USE [GestioneIndirizzi]
GO
/****** Object:  Table [dbo].[Billing_Address]    Script Date: 13/05/2018 17:05:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Billing_Address](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[label] [varchar](max) NOT NULL,
	[first_name] [varchar](30) NOT NULL,
	[last_name] [varchar](30) NOT NULL,
	[company] [varchar](50) NOT NULL,
	[address] [varchar](60) NOT NULL,
	[city] [varchar](25) NOT NULL,
	[country] [varchar](25) NOT NULL,
	[province] [varchar](2) NOT NULL,
	[postal_code] [varchar](10) NOT NULL,
	[phone] [varchar](15) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[vat_code] [varchar](50) NOT NULL,
	[fiscal_code] [varchar](50) NOT NULL,
	[tenant_id] [int] NOT NULL,
 CONSTRAINT [PK_Table_2_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Billing_Address_Audit]    Script Date: 13/05/2018 17:05:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Billing_Address_Audit](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_billing] [int] NOT NULL,
	[first_name] [varchar](30) NULL,
	[last_name] [varchar](30) NULL,
	[company] [varchar](50) NULL,
	[address] [varchar](60) NULL,
	[city] [varchar](25) NULL,
	[country] [varchar](25) NULL,
	[province] [varchar](2) NULL,
	[postal_code] [varchar](10) NULL,
	[phone] [varchar](15) NULL,
	[email] [varchar](50) NULL,
	[vat_code] [varchar](50) NULL,
	[fiscal_code] [varchar](50) NULL,
	[date_upd] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LogInfo]    Script Date: 13/05/2018 17:05:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LogInfo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[msg] [varchar](max) NOT NULL,
	[log_type] [varchar](50) NOT NULL,
	[date] [date] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Shipping_Address]    Script Date: 13/05/2018 17:05:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Shipping_Address](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[label] [varchar](max) NOT NULL,
	[first_name] [varchar](30) NOT NULL,
	[last_name] [varchar](30) NOT NULL,
	[company] [varchar](50) NOT NULL,
	[address] [varchar](60) NOT NULL,
	[city] [varchar](25) NOT NULL,
	[country] [varchar](25) NOT NULL,
	[province] [varchar](2) NOT NULL,
	[postal_code] [varchar](10) NOT NULL,
	[phone] [varchar](15) NOT NULL,
	[tenant_id] [int] NOT NULL,
 CONSTRAINT [PK_Table_1_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Shipping_Address_Audit]    Script Date: 13/05/2018 17:05:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Shipping_Address_Audit](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_shipping] [int] NOT NULL,
	[first_name] [varchar](30) NULL,
	[last_name] [varchar](30) NULL,
	[company] [varchar](50) NULL,
	[address] [varchar](60) NULL,
	[city] [varchar](25) NULL,
	[country] [varchar](25) NULL,
	[province] [varchar](2) NULL,
	[postal_code] [varchar](10) NULL,
	[phone] [varchar](15) NULL,
	[date_upd] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tenant]    Script Date: 13/05/2018 17:05:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tenant](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tenant_id] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Billing_Address]  WITH CHECK ADD  CONSTRAINT [FK_Table_2_Tenant] FOREIGN KEY([tenant_id])
REFERENCES [dbo].[Tenant] ([id])
GO
ALTER TABLE [dbo].[Billing_Address] CHECK CONSTRAINT [FK_Table_2_Tenant]
GO
ALTER TABLE [dbo].[Shipping_Address]  WITH CHECK ADD  CONSTRAINT [FK_Table_1_Tenant] FOREIGN KEY([tenant_id])
REFERENCES [dbo].[Tenant] ([id])
GO
ALTER TABLE [dbo].[Shipping_Address] CHECK CONSTRAINT [FK_Table_1_Tenant]
GO
