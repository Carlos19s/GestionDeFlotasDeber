
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Sensores.Modelos;
using System.Collections.Generic;

namespace Proyecto.Api.MVC.Models
{
    public class Reporte
    {
        public static byte[] Descargar(IEnumerable<Alerta> alertas)
        {
            try
            {
                // Validamos si alertas es null o vacío
                if (alertas == null || !alertas.Any())
                {
                    return Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Margin(30);

                        // Encabezado
                        page.Header()
                            .Text("No Hay Alertas")
                            .FontSize(18)
                            .SemiBold()
                            .FontColor(Colors.Blue.Medium)
                            .AlignCenter();
                    });
                }).GeneratePdf();
                }

                return Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Margin(30);

                        // Encabezado
                        page.Header()
                            .Text("Reporte de Alertas de Mantenimiento Predictivo")
                            .FontSize(18)
                            .SemiBold()
                            .FontColor(Colors.Blue.Medium)
                            .AlignCenter();

                        // Contenido (tabla)
                        page.Content().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1); // Camión
                                columns.RelativeColumn(3); // Descripción
                                columns.RelativeColumn(1); // Fecha
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Camión");
                                header.Cell().Element(CellStyle).Text("Descripción");
                                header.Cell().Element(CellStyle).Text("Fecha");
                            });

                            // Verifica que la lista de alertas no sea null ni vacía
                            foreach (var alerta in alertas)
                            {
                                if (alerta == null)
                                {
                                    continue; // O puedes lanzar un error si alguna alerta es null
                                }

                                table.Cell().Element(CellStyle).Text(alerta.IdCamion.ToString());
                                table.Cell().Element(CellStyle).Text(alerta.Mensaje);
                                table.Cell().Element(CellStyle).Text(alerta.Fecha.ToString("dd/MM/yyyy"));
                            }

                            static IContainer CellStyle(IContainer container) =>
                                container.Padding(5)
                                         .BorderBottom(1)
                                         .BorderColor(Colors.Grey.Lighten2);
                        });

                        // Pie de página con número de página
                        page.Footer()
                            .AlignCenter()
                            .Text(text =>
                            {
                                text.Span("Página ");
                                text.CurrentPageNumber();
                                text.Span(" de ");
                                text.TotalPages();
                            });
                    });
                }).GeneratePdf();
            }
            catch (Exception ex)
            {
                // Logueamos el error detallado
                Console.WriteLine($"Error generando el PDF: {ex.Message}");
                throw; // Lanzamos la excepción nuevamente para que se maneje en el controlador
            }
        }


    }
}
