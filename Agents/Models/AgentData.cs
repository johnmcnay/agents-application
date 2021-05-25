using Microsoft.Extensions.Configuration;
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

                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM Agents";

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var name = reader["AgentName"].ToString();
                    var workingArea = reader["WorkingArea"].ToString();
                    var phoneNumber = reader["PhoneNo"].ToString();
                    var agentCode = reader["AgentCode"].ToString();

                    agents.Add(new Agent
                    {
                        name = name,
                        workingArea = workingArea,
                        phoneNumber = phoneNumber,
                        agentCode = agentCode
                    });
                }
            }

            return agents;
        }
    }
}