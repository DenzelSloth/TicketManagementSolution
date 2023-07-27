using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TR_Ticket_ProcessResumen_Trigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string tr_Ticket_ProcessResumen = @"
			SET ANSI_NULLS ON
			GO

			SET QUOTED_IDENTIFIER ON
			GO

			CREATE TRIGGER [dbo].[TR_Ticket_ProcessResumen]
			   ON  [dbo].[Tickets]
			   AFTER INSERT
			AS 
			BEGIN
				SET NOCOUNT ON;
					DECLARE @InsertedId_Tienda VARCHAR(10);
					DECLARE @InsertedId_Registradora VARCHAR(10);

					SELECT @InsertedId_Tienda = [Id_Tienda], @InsertedId_Registradora = [Id_Registradora]
					FROM inserted;

    				  IF NOT EXISTS (
						SELECT TOP 1 1
						FROM [dbo].[Resumen]
						WHERE [Id_Tienda] = @InsertedId_Tienda AND [Id_Registradora] = @InsertedId_Registradora
					)
					BEGIN
					INSERT INTO [dbo].[Resumen]
							   ([Id_Tienda]
							   ,[Id_Registradora]
							   ,[Tickets])
						 VALUES
							   (@InsertedId_Tienda
							   ,@InsertedId_Registradora
							   ,0)
					END

					UPDATE [dbo].[Resumen]
					SET [Tickets] = (
						SELECT COUNT(*)
						FROM [dbo].[Tickets]
						WHERE [Id_Tienda] = @InsertedId_Tienda AND [Id_Registradora] = @InsertedId_Registradora
					)
					WHERE [Id_Tienda] = @InsertedId_Tienda AND [Id_Registradora] = @InsertedId_Registradora

			END
			GO

			ALTER TABLE [dbo].[Tickets] ENABLE TRIGGER [TR_Ticket_ProcessResumen]
			GO";
            migrationBuilder.Sql(tr_Ticket_ProcessResumen);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string tr_Ticket_ProcessResumenDrop = @"DROP TRIGGER [dbo].[TR_Ticket_ProcessResumen]";
            migrationBuilder.Sql(tr_Ticket_ProcessResumenDrop);
        }
    }
}
