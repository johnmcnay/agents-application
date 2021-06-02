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

        public List<Agent> ActiveAgentData()
        {

            var agents = new List<Agent>();

            var connString = _configuration.GetConnectionString("default");
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                string CommandText = "SELECT * FROM Agents WHERE isActive = 1";

                SqlCommand dbCommand = new SqlCommand(CommandText, conn);
                var reader = dbCommand.ExecuteReader();

                while (reader.Read())
                {
                    agents.Add(new Agent(reader));
                }
            }

            return agents;

        }

        internal void EditAgent(Agent agent, string originalCode)
        {
            var connString = _configuration.GetConnectionString("default");
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                string CommandText = "UPDATE Agents SET " +
                    "AgentCode = @agentCode, " +
                    "AgentName = @name, " +
                    "WorkingArea = @workingArea, " +
                    "Commission = @commission, " +
                    "PhoneNo = @phoneNumber WHERE AgentCode = @originalCode";

                SqlCommand dbCommand = new SqlCommand(CommandText, conn);
                dbCommand.Parameters.AddWithValue("@agentCode", agent.agentCode);
                dbCommand.Parameters.AddWithValue("@name", agent.name);
                dbCommand.Parameters.AddWithValue("@workingArea", agent.workingArea);
                dbCommand.Parameters.AddWithValue("@commission", agent.commission);
                dbCommand.Parameters.AddWithValue("@phoneNumber", agent.phoneNumber);
                dbCommand.Parameters.AddWithValue("@originalCode", originalCode);
                dbCommand.ExecuteNonQuery();
            }
        }

        internal void DeleteAgent(string id)
        {
            var connString = _configuration.GetConnectionString("default");
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                string CommandText = "UPDATE Agents SET isActive = 0 WHERE AgentCode = @agentCode";

                SqlCommand dbCommand = new SqlCommand(CommandText, conn);
                dbCommand.Parameters.AddWithValue("@agentCode", id);
                dbCommand.ExecuteNonQuery();
            }
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

        internal void AddAgent(Agent agent)
        {
            var connString = _configuration.GetConnectionString("default");
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                string CommandText = "INSERT INTO Agents VALUES (@agentCode, @name, @workingArea, @commission, @phoneNumber, @isActive);";

                SqlCommand dbCommand = new SqlCommand(CommandText, conn);
                dbCommand.Parameters.AddWithValue("@agentCode", agent.agentCode);
                dbCommand.Parameters.AddWithValue("@name", agent.name);
                dbCommand.Parameters.AddWithValue("@workingArea", agent.workingArea);
                dbCommand.Parameters.AddWithValue("@commission", agent.commission);
                dbCommand.Parameters.AddWithValue("@phoneNumber", agent.phoneNumber);
                dbCommand.Parameters.AddWithValue("@isActive", 1);
                dbCommand.ExecuteNonQuery();
            }
        }
    }
}