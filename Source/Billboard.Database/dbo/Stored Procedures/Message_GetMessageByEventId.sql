-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Message_GetMessageByEventId]
(
	@EventId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Declare @Now datetime 
	Set @Now = GETUTCDATE()

	Declare @Number nvarchar(50)

    -- Insert statements for procedure here
  	Set @Number = (Select Number
	From [Event] E
	Where StartTime <= @Now AND EndTime >= @Now AND E.Id = @EventId)

	IF @Number Is Not Null
	Begin
		
	Select M.*
	From [Message] M
	Inner join [Event] E
		ON M.[To] = E.Number
	Where E.StartTime <= @Now AND E.EndTime >= @Now


		
	End
	

END