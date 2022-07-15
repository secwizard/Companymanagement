GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'SP_SaveUpdateCompanyTemplateSection') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE SP_SaveUpdateCompanyTemplateSection
END
GO
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
CREATE PROCEDURE [dbo].[SP_SaveUpdateCompanyTemplateSection]  --SP_SaveUpdateCompanyTemplateSection 0,153,2,'Hello',null,1,'EE030000-0000-0000-0000-000000000000','2021-09-07 22:44:33.167',null,null,null,null,7,4    
 @CompanyTemplateSectionId int,    
 @CompanyTemplateId int,    
 @SectionType int,    
 @SectionName nvarchar(500),    
 @SectionBackgrounColor nvarchar(100) = null,    
 @IsActive bit,    
 @CreatedBy nvarchar(max),    
 @UpdatedBy nvarchar(max) = null,    
 @PrimaryText nvarchar(max) = null,    
 @SecondaryText nvarchar(max) = null,    
 @TertiaryText nvarchar(max) = null,    
 @DisplayOrder int,    
 @SectionFor int    
AS    
BEGIN    
 declare @MaxDisplayOrder int = isnull((select max(DisplayOrder) from CompanyTemplateSection where CompanyTemplateId = @CompanyTemplateId),0)
 if(@CompanyTemplateSectionId = 0)    
 BEGIN    
 INSERT INTO [dbo].[CompanyTemplateSection]    
           ([CompanyTemplateId]    
			,[SectionType]    
           ,[SectionName]    
           ,[SectionBackgrounColor]    
           ,[IsActive]    
           ,[CreatedBy]    
           ,[CreatedAt]    
           ,[PrimaryText]    
           ,[SecondaryText]    
           ,[TertiaryText]    
           ,[DisplayOrder]    
           ,[SectionFor])    
     VALUES    
           (@CompanyTemplateId    
     ,@SectionType    
           ,@SectionName    
           ,@SectionBackgrounColor    
           ,@IsActive    
           ,@CreatedBy    
           ,GETDATE()    
           ,@PrimaryText    
           ,@SecondaryText    
           ,@TertiaryText    
           ,@MaxDisplayOrder    
           ,@SectionFor)    
      set @CompanyTemplateSectionId = Scope_Identity();    
 END    
 else    
 BEGIN    
 UPDATE [CompanyTemplateSection]    
 set    
      CompanyTemplateId = @CompanyTemplateId    
           ,SectionType = @SectionType    
           ,SectionName = @SectionName    
           ,SectionBackgrounColor = @SectionBackgrounColor    
           ,IsActive = @IsActive    
           ,UpdatedBy = @UpdatedBy    
           ,UpdatedAt = GETDATE()    
           ,PrimaryText = @PrimaryText    
           ,SecondaryText = @SecondaryText    
           ,TertiaryText = @TertiaryText    
           ,DisplayOrder = @DisplayOrder    
           ,SectionFor = @SectionFor    
     where CompanyTemplateSectionId = @CompanyTemplateSectionId    
     
END    
 Select * from CompanyTemplateSection where  CompanyTemplateSectionId = @CompanyTemplateSectionId    
    
END    
GO
