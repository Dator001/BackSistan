using Microsoft.AspNetCore.Mvc;
using PruebaSistranLatam.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;


namespace PruebaSistranLatam.Data
{
    public class ClienteData
    {
        private readonly string conexion;

        public ClienteData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }
        public async Task<List<Cliente>> Lista()
        {
            List<Cliente> lista = new List<Cliente>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SSP_GETCLIENTES", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Cliente
                        {
                            idCliente = (int)reader["IDCLIENTE"],
                            nombre = reader["NOMBRE"].ToString(),
                            apellido = reader["APELLIDO"].ToString(),
                            idTipoDocumento = (int)reader["IDTIPODOCUMENTO"],
                            numeroDocumento = reader["NUMERODOCUMENTO"].ToString(),
                            fechaNaciomiento = reader["FECHANACIMIENTO"] as DateTime?,
                            telefono1 = reader["TELEFONO1"].ToString(),
                            telefono2 = reader["TELEFONO2"].ToString(),
                            correo1 = reader["CORREO1"].ToString(),
                            correo2 = reader["CORREO2"].ToString(),
                            nombreCiudad = reader["NOMBRECIUDAD"].ToString(),
                            direccion1 = reader["DIRECCION1"].ToString(),
                            direccion2 = reader["DIRECCION2"].ToString()
                        });
                    }
                }
            }
            return lista;
        }


        public async Task<Cliente> ObtenerCliente(int id)
        {
            Cliente objeto = new Cliente();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SSP_GETCLIENTEBYID", con);
                cmd.Parameters.AddWithValue("@INIDCLIENTE", id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new Cliente
                        {
                            idCliente = (int)reader["IDCLIENTE"],
                            nombre = reader["NOMBRE"].ToString(),
                            apellido = reader["APELLIDO"].ToString(),
                            tipoDocumento = reader["TIPODOCUMENTO"].ToString(),
                            idTipoDocumento = (int)reader["IDTIPODOCUMENTO"],
                            numeroDocumento = reader["NUMERODOCUMENTO"].ToString(),
                            fechaNaciomiento = reader["FECHANACIMIENTO"] as DateTime?,
                            telefono1 = reader["TELEFONO1"].ToString(),
                            telefono2 = reader["TELEFONO2"].ToString(),
                            correo1 = reader["CORREO1"].ToString(),
                            correo2 = reader["CORREO2"].ToString(),
                            nombreCiudad = reader["NOMBRECIUDAD"].ToString(),
                            idCiudad = (int)reader["IDCIUDAD"],
                            direccion1 = reader["DIRECCION1"].ToString(),
                            direccion2 = reader["DIRECCION2"].ToString()
                        };
                    }
                }
            }
            return objeto; 
        }


        public async Task<bool> CrearCliente(Cliente objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SSP_INSCLIENTES", con);
                cmd.Parameters.AddWithValue("@INNOMBRE", objeto.nombre);
                cmd.Parameters.AddWithValue("@INAPELLIDO", objeto.apellido);
                cmd.Parameters.AddWithValue("@INTIPODOCUMENTO", objeto.idTipoDocumento);
                cmd.Parameters.AddWithValue("@INNUMERODOCUMENTO", objeto.numeroDocumento);
                cmd.Parameters.AddWithValue("@INFECHANACIMIENTO", objeto.fechaNaciomiento);
                cmd.Parameters.AddWithValue("@INTELEFONO1", objeto.telefono1);
                cmd.Parameters.AddWithValue("@INTELEFONO2", objeto.telefono2);
                cmd.Parameters.AddWithValue("@INCORREO1", objeto.correo1);
                cmd.Parameters.AddWithValue("@INCORREO2", objeto.correo2);
                cmd.Parameters.AddWithValue("@INCIUDAD", objeto.idCiudad);
                cmd.Parameters.AddWithValue("@INDIRECCION1", objeto.direccion1);
                cmd.Parameters.AddWithValue("@INDIRECCION2", objeto.direccion2);
                SqlParameter mensajeParam = new SqlParameter("@MENSAJE", SqlDbType.VarChar, 200);
                mensajeParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(mensajeParam);
                SqlParameter ultimoParam = new SqlParameter("@ULTIMO", SqlDbType.Int);
                ultimoParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ultimoParam);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                        respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? false : true;
                }
                catch 
                {
                    respuesta = false;
                }
            }   

            return respuesta;
        }


        public async Task<bool> EditarCliente(Cliente objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SSP_SETCLIENTES", con);
                cmd.Parameters.AddWithValue("@INIDCLIENTE", objeto.idCliente);
                cmd.Parameters.AddWithValue("@INNOMBRE", objeto.nombre);
                cmd.Parameters.AddWithValue("@INAPELLIDO", objeto.apellido);
                cmd.Parameters.AddWithValue("@INTELEFONO1", objeto.telefono1);
                cmd.Parameters.AddWithValue("@INTELEFONO2", objeto.telefono2);
                cmd.Parameters.AddWithValue("@INCORREO1", objeto.correo1);
                cmd.Parameters.AddWithValue("@INCORREO2", objeto.correo2);
                cmd.Parameters.AddWithValue("@INCIUDAD", objeto.idCiudad);
                cmd.Parameters.AddWithValue("@INDIRECCION1", objeto.direccion1);
                cmd.Parameters.AddWithValue("@INDIRECCION2", objeto.direccion2);


                SqlParameter mensajeParam = new SqlParameter("@MENSAJE", SqlDbType.VarChar, 200);
                mensajeParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(mensajeParam);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? false : true;
                }
                catch (Exception)
                {

                    respuesta = false;
                }

            }
            return respuesta;
        }



        public async Task<bool> EliminarCliente(int id)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();

                // Crear el comando para eliminar el cliente
                using (var cmd = new SqlCommand("SSP_DELCLIENTES", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@INIDCLIENTES", id);

                    // Agregar parámetro de salida para el mensaje
                    var mensajeParam = new SqlParameter("@MENSAJE", SqlDbType.VarChar, 200);
                    mensajeParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(mensajeParam);

                    try
                    {
                        respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? false : true;
                    }
                    catch 
                    {
                        respuesta = false;
                    }
                }
            }
            return respuesta;
        }
    }
}
