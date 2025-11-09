using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace Motos.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/db")]
    [ApiController]
    public class DbController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DbController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("listar-motos")]
        public async Task<IActionResult> ListarMotosDoBanco()
        {
            var connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))) (CONNECT_DATA=(SERVER=DEDICATED)(SID=ORCL)));User Id=rm558961;Password=fiap25;";
            var linhas = new List<string>();

            try
            {
                using var conn = new OracleConnection(connectionString);
                await conn.OpenAsync();

                using (var cmdEnable = conn.CreateCommand())
                {
                    cmdEnable.CommandText = "BEGIN DBMS_OUTPUT.ENABLE(1000000); END;";
                    await cmdEnable.ExecuteNonQueryAsync();
                }

                using (var cmdProc = conn.CreateCommand())
                {
                    cmdProc.CommandText = "BEGIN pkg_motos.sp_listar_motos_json_completo; END;";
                    await cmdProc.ExecuteNonQueryAsync();
                }

                using var cmdRead = conn.CreateCommand();
                cmdRead.CommandText = "BEGIN DBMS_OUTPUT.GET_LINE(:line, :status); END;";
                cmdRead.Parameters.Add("line", OracleDbType.Varchar2, 32767).Direction = System.Data.ParameterDirection.Output;
                cmdRead.Parameters.Add("status", OracleDbType.Int32).Direction = System.Data.ParameterDirection.Output;

                while (true)
                {
                    await cmdRead.ExecuteNonQueryAsync();

                    var status = Convert.ToInt32(((OracleDecimal)cmdRead.Parameters["status"].Value).Value);
                    if (status != 0)
                        break;

                    string line = cmdRead.Parameters["line"].Value.ToString()?.Trim();
                    if (!string.IsNullOrWhiteSpace(line) && line.StartsWith("{"))
                        linhas.Add(line);
                }

                if (linhas.Count == 0)
                    return NotFound(new { mensagem = "Nenhuma moto encontrada no banco ou saída inválida." });

                var jsonArray = "[" + string.Join(",", linhas) + "]";

                return Content(jsonArray, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message });
            }
        }
    }
}
