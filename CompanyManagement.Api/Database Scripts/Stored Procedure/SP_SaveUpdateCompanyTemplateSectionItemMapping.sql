GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'SP_SaveUpdateCompanyTemplateSectionItemMapping') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE SP_SaveUpdateCompanyTemplateSectionItemMapping
END
GO
CREATE PROCEDURE [dbo].[SP_SaveUpdateCompanyTemplateSectionItemMapping]  --SP_SaveUpdateCompanyTemplateSectionItemMapping 9,'1734,1735,',2,1,'admin','admin'        
 @CompanyTemplateSectionId int,        
 @SectionCustomId as nvarchar(max),        
 @IsActive bit,        
 @CreatedBy nvarchar(max),        
 @UpdatedBy nvarchar(max) = null        
AS        
BEGIN        
 DECLARE @sSectionCustomId nvarchar(max) = @SectionCustomId        
 Update CompanyTemplateSectionItemMapping set isActive = 0 where CompanyTemplateSectionId = @CompanyTemplateSectionId        
  Update [CompanyTemplateSectionImageMapping] set isActive = 0 where CompanyTemplateSectionId = @CompanyTemplateSectionId        
    
   declare @IdAndOrder table        
 (        
  RowId int        
  , Value nvarchar(MAX)        
 )        
        
 insert into @IdAndOrder        
 select * from dbo.Split(',', @sSectionCustomId)        
        
 declare @RowId int, @Value nvarchar(MAX)        
 declare crOrder cursor for        
 select RowId, Value from @IdAndOrder        
 open crOrder        
 fetch next from crOrder into @RowId, @Value        
 while @@fetch_status = 0        
 begin        
      
 INSERT INTO CompanyTemplateSectionItemMapping        
 (        
  [CompanyTemplateSectionId]        
 ,[ItemId]        
 ,[DisplayOrder]      
 ,[IsActive]        
 ,[CreatedBy]        
 ,[CreatedAt]        
 )        
  select           
           @CompanyTemplateSectionId        
     ,(select Value from dbo.Split('#', @Value) where RowId = 1)        
           ,(select Value from dbo.Split('#', @Value) where RowId = 2)          
           ,@IsActive         
           ,@CreatedBy         
           ,GETDATE()         
      
      
  INSERT INTO [dbo].[CompanyTemplateSectionImageMapping]      
           ([CompanyTemplateSectionId]      
           ,[ImagePath]      
           ,[DisplayOrder]      
           ,[IsActive]      
           ,[CreatedBy]      
           ,[CreatedAt]      
           ,[ItemId])      
     select           
           @CompanyTemplateSectionId        
     ,(select Value from dbo.Split('#', @Value) where RowId = 3)        
           ,(select Value from dbo.Split('#', @Value) where RowId = 2)          
           ,@IsActive         
           ,@CreatedBy         
           ,GETDATE()         
     ,(select Value from dbo.Split('#', @Value) where RowId = 1)     
      
      
        
  fetch next from crOrder into @RowId, @Value        
 end        
 close crOrder        
 deallocate crOrder        
 Select ItemId from CompanyTemplateSectionItemMapping where  CompanyTemplateSectionId = @CompanyTemplateSectionId  and IsActive = 1        
           
END 
GO
