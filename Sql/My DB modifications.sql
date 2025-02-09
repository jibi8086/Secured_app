USE [SendSecurely]
GO
/****** Object:  Table [dbo].[FileDetail]    Script Date: 3/28/2020 1:28:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[FileName] [varchar](150) NULL,
	[FilePath] [varchar](150) NULL,
	[FilePassword] [varchar](20) NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_FileDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[FileDetail] ON 

INSERT [dbo].[FileDetail] ([ID], [UserId], [FileName], [FilePath], [FilePassword], [CreatedDate]) VALUES (3, 1, N'1081744863 export.pdf', N'D:\New folder\Secure_App\PdfEncryption\wwwroot\AppData\', N'syc7657PV', CAST(N'2020-03-27T19:52:53.750' AS DateTime))
INSERT [dbo].[FileDetail] ([ID], [UserId], [FileName], [FilePath], [FilePassword], [CreatedDate]) VALUES (4, 1, N'1804998650 si of police-armed police battalian.pdf', N'D:\New folder\Secure_App\PdfEncryption\wwwroot\AppData\', N'qsj5426XV', CAST(N'2020-03-27T19:56:54.390' AS DateTime))
SET IDENTITY_INSERT [dbo].[FileDetail] OFF
ALTER TABLE [dbo].[FileDetail] ADD  CONSTRAINT [df_ConstraintNAme]  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
/****** Object:  StoredProcedure [dbo].[Sp_InsertFiledetails]    Script Date: 3/28/2020 1:28:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Sp_InsertFiledetails] 
	-- Add the parameters for the stored procedure here
	@UserId int,
	@FileName varchar(150),
	@FilePath varchar(150),
	@FilePassword varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into FileDetail(UserId,FileName,FilePath,FilePassword) values(@UserId,@FileName,@FilePath,@FilePassword)
END
GO
