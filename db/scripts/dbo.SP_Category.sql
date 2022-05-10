/*
return all Categories order by category name
filters: category id or name
*/

CREATE   PROCEDURE [dbo].[SP_Category]
	@category_id int = 0,      
	@category_name nchar(200) = ''

AS

begin 
	SELECT c.Id categoryId, LTrim(RTrim(c.Name)) categoryName 
	  FROM  Category AS c 
     WHERE ((c.Id =@category_id or @category_id = 0) and (Upper(LTrim(RTrim(c.Name))) Like '%' + Upper(LTrim(RTrim(@category_name))) + '%' or  @category_name = ''))
    ORDER BY c.Name

end