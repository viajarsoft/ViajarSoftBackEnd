create proc sp_CrearToken
	@usuario varchar(20),
	@token varchar(100),
	@fechaVencimiento smalldatetime
AS	
Begin
	Insert  into dbo.Tokens (Token,Usuario,FechaVencimiento)
	values (@token,@usuario,@fechaVencimiento)
	
	Select * from dbo.Tokens where IdToken = @@IDENTITY;
end
GO
-- Prueba
Declare 
	@fecha smalldatetime
set @fecha = GETDATE()
exec sp_CrearToken 'prueba','tokenLargo', @fecha