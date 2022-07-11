GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'SP_GetSocialLinkById') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE SP_GetSocialLinkById
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetSocialLinkById]
(      
@CompanyId bigint,      
@CompanySocialLinkId bigint
)    
AS
BEGIN
	
	SELECT [CompanySocialLinkId] as CompanySocialLinkId,  
 [CompanyId] as CompanyId,  
 [IsActive] as IsActive,  
 [Facebook] as Facebook,  
 [ShowFacebookOnline] as ShowFacebookOnline,  
 [Instagram] as Instagram,  
 [ShowInstagramOnline] as ShowInstagramOnline,  
 [Twitter] as Twitter,  
 [ShowTwitterOnline] as ShowTwitterOnline,  
 [ContactEmail] as ContactEmail,  
 [ShowContactEmailOnline] as ShowContactEmailOnline,  
 [ContactPhone] as ContactPhone,  
 [ShowContactPhoneOnline] as ShowContactPhoneOnline,  
 [CreatedByUserId] as CreatedByUserId,  
 [CreatedAt] as CreatedAt,  
 [UpdatedByUserID] as UpdatedByUserID,  
 [UpdatedAt] as UpdatedAt   
 FROM [CompanySocialLink] CSL    
 WHERE CSL.CompanyId = @CompanyId    
 AND (CSL.IsActive = 1) AND CompanySocialLinkId = @CompanySocialLinkId   
END

GO
