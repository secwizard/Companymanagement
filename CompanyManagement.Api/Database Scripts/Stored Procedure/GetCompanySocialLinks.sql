GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'GetCompanySocialLinks') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE GetCompanySocialLinks
END
GO
-- =============================================    
-- Author:  Pradip Nag    
-- Create date: 02 apr 2022    
-- Description: Get company Social Links    
-- =============================================    
CREATE PROCEDURE [dbo].[GetCompanySocialLinks]    
(    
 @CompanyId Bigint    
)    
AS    
BEGIN    
 SET NOCOUNT ON;    
    
 SELECT    
   [CompanySocialLinkId],  
 [CompanyId] ,  
 [IsActive] ,  
 [Facebook] ,  
 [ShowFacebookOnline] ,  
 [Instagram],  
 [ShowInstagramOnline] ,  
 [Twitter],  
 [ShowTwitterOnline] ,  
 [ContactEmail] ,  
 [ShowContactEmailOnline],  
 [ContactPhone],  
 [ShowContactPhoneOnline],  
 [CreatedByUserId],  
 [CreatedAt],  
 [UpdatedByUserID] ,  
 [UpdatedAt]   
 FROM [dbo].[CompanySocialLink] CSL    
 WHERE CSL.CompanyId = @CompanyId    
 AND (CSL.IsActive = 1)    
END 
GO
