CREATE PROCEDURE [dbo].[DisplayProvinces]
AS
BEGIN
	SELECT [ProvinceID], [ProvinceName]
	FROM Provinces
END
