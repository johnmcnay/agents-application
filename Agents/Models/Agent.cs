using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Agents.Models
{
    public class Agent
    {

        public Agent(SqlDataReader reader)
        {
            name = reader["AgentName"].ToString();
            workingArea = reader["WorkingArea"].ToString();
            phoneNumber = reader["PhoneNo"].ToString();
            agentCode = reader["AgentCode"].ToString();
            commission = System.Convert.ToSingle(reader["Commission"]);
        }

        public Agent()
        {

        }

        public string agentCode { get; set; }

        public string name { get; set; }

        public string workingArea { get; set; }

        public float commission { get; set; }

        public string phoneNumber { get; set; }
        
    }
}