SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrderStateData](
	[CorrelationId] [uniqueidentifier] NOT NULL,
	[CurrentState] [nvarchar](max) NULL,
	[OrderCreationDateTime] [datetime] NULL,
	[OrderCancelDateTime] [datetime] NULL,
	[OrderId] [uniqueidentifier] NULL,
	[Price] [decimal](18, 0) NULL,
	[ProductName] [nvarchar](max) NULL,
	[Mobile] [nvarchar](20) NULL,
 CONSTRAINT [PK_OrderStateData] PRIMARY KEY CLUSTERED 
(
	[CorrelationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


