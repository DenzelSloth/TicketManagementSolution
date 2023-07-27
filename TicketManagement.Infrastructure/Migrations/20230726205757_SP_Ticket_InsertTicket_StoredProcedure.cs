using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SP_Ticket_InsertTicket_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_InsertTicket = @"
			SET ANSI_NULLS ON
			GO

			SET QUOTED_IDENTIFIER ON
			GO
						CREATE   PROCEDURE [dbo].[SP_Ticket_InsertTicket]
							@Id_Tienda VARCHAR(10),
							@Id_Registradora VARCHAR(10),
							@FechaHora datetime, 
							@Ticket int,
							@Impuesto money,
							@Total money
						AS
						BEGIN
							SET NOCOUNT ON;

							IF (@Id_Tienda IS NULL OR @Id_Registradora IS NULL OR @FechaHora IS NULL OR @Ticket IS NULL OR @Impuesto IS NULL OR @Total IS NULL)
							BEGIN
								RAISERROR (15600, -1, -1, 'SP_Ticket_InsertTicket');
								RETURN;
							END

							  IF EXISTS (
									SELECT TOP 1 1
									FROM [dbo].[Tickets]
									WHERE [Ticket] = @Ticket
								)
								BEGIN
									RAISERROR('ERROR INSERTING DATA, DUPLICATED TICKET NUMBER.', 16, 1, 'SP_Ticket_InsertTicket');
									RETURN;
								END

							BEGIN TRANSACTION;

							INSERT INTO [dbo].[Tickets]
									   ([Id_Tienda]
									   ,[Id_Registradora]
									   ,[FechaHora]
									   ,[Ticket]
									   ,[Impuesto]
									   ,[Total]
									   ,[FechaHora_Creacion])
								 VALUES
									   (@Id_Tienda
									   ,@Id_Registradora
									   ,@FechaHora
									   ,@Ticket
									   ,@Impuesto
									   ,@Total
									   ,GETDATE());
	
							IF @@ERROR <> 0
							BEGIN
								ROLLBACK TRANSACTION;
								RAISERROR('ERROR INSERTING DATA.', 16, 1);
								RETURN;
							END

							COMMIT TRANSACTION;
						END
			GO";
            migrationBuilder.Sql(sp_InsertTicket);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_InsertTicketDrop = @"DROP PROCEDURE [dbo].[SP_Ticket_InsertTicket]";
            migrationBuilder.Sql(sp_InsertTicketDrop);
        }
    }
}
