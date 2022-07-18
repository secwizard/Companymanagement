GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'SP_SaveUpdateSocialLink') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE SP_SaveUpdateSocialLink
END
GO
CREATE PROCEDURE [dbo].[SP_SaveUpdateSocialLink] 
	-- Add the parameters for the stored procedure here
	@CompanySocialLinkId bigint,
	@CompanyId bigint,
	@IsActive bit,
	@Facebook nvarchar(max),
	@ShowFacebookOnline bit,
	@Instagram nvarchar(max),
	@ShowInstagramOnline bit,
	@Twitter nvarchar(max),
	@ShowTwitterOnline bit,
	@ContactEmail nvarchar(max) = null,
	@ShowContactEmailOnline bit = null,
	@ContactPhone nvarchar(max),
	@ShowContactPhoneOnline bit,
	@CreatedByUserId uniqueidentifier,
	@CreatedAt datetime,
	@UpdatedByUserID uniqueidentifier = Null,
	@UpdatedAt datetime = Null

AS
BEGIN
declare @companyFlag int=0;
if exists(select CompanyId from CompanySocialLink where CompanySocialLinkId = @CompanySocialLinkId )
begin 
set @companyFlag = 1
end;
else
begin 
set @companyFlag = 0
end;
	if(@CompanySocialLinkId = 0)
	BEGIN
	INSERT INTO CompanySocialLink
	(
	[CompanyId]
	,[IsActive]
	,[Facebook]
	,[ShowFacebookOnline]
	,[Instagram]
	,[ShowInstagramOnline]
	,[Twitter]
	,[ShowTwitterOnline]
	,[ContactEmail]
	,[ShowContactEmailOnline]
	,[ContactPhone]
	,[ShowContactPhoneOnline]
	,[CreatedByUserId]
	,[CreatedAt]
	)
	VALUES(
	@CompanyId
	,@IsActive
	,@Facebook
	,@ShowFacebookOnline
	,@Instagram
	,@ShowInstagramOnline
	,@Twitter
	,@ShowTwitterOnline
	,@ContactEmail
	,@ShowContactEmailOnline
	,@ContactPhone
	,@ShowContactPhoneOnline
	,@CreatedByUserId
	,GETDATE()
	)
	 set @CompanySocialLinkId = Scope_Identity();
	END
	else
	BEGIN
	UPDATE CompanySocialLink
	set
	IsActive = @IsActive
	,Facebook = @Facebook
	,ShowFacebookOnline = @ShowFacebookOnline
	,Instagram = @Instagram
	,ShowInstagramOnline = @ShowInstagramOnline
	,Twitter = @Twitter
	,ShowTwitterOnline = @ShowTwitterOnline
	,ContactEmail = @ContactEmail
	,ShowContactEmailOnline = @ShowContactEmailOnline
	,ContactPhone = @ContactPhone
	,ShowContactPhoneOnline = @ShowContactPhoneOnline
	,UpdatedByUserID = @UpdatedByUserID
	,UpdatedAt = GETDATE()
	where CompanySocialLinkId = @CompanySocialLinkId
	
END
Select * from CompanySocialLink where CompanyId = @CompanyId and CompanySocialLinkId = @CompanySocialLinkId
end
GO
