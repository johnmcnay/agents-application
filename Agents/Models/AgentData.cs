using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Agents.Models
{
    public class AgentData
    {
        private readonly IConfiguration _configuration;

        public AgentData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Agent> AllAgentData()
        {
            var agents = new List<Agent>();

            var connString = _configuration.GetConnectionString("default");
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                string CommandText = "SELECT * FROM Agents";

                SqlCommand dbCommand = new SqlCommand(CommandText, conn);
                var reader = dbCommand.ExecuteReader();
                
                while (reader.Read())
                {
                    agents.Add(new Agent(reader));
                }
            }

            return agents;
        }

        public Agent GetOne(string agentCode)
        {

            var connString = _configuration.GetConnectionString("default");
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                string CommandText = @"SELECT * FROM Agents WHERE AgentCode = @agentCode";

                SqlCommand dbCommand = new SqlCommand(CommandText, conn);
                dbCommand.Parameters.AddWithValue("@agentCode", agentCode);
                
                var reader = dbCommand.ExecuteReader();
                reader.Read();

                return new Agent(reader);
            }
        }
    }
}