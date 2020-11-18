using LeftFacingAardvark.EF;
using LeftFacingAardvark.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LeftFacingAardvark.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgentController : ControllerBase
    {
        private readonly ILogger<AgentController> _logger;
        private AardvarkContext _db;

        public AgentController(ILogger<AgentController> logger, AardvarkContext db)
        {
            _logger = logger;
            _db = db;
        }

        /// <summary>
        /// Gets a Basic Summary of all Agents
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Agent>), 200)]
        public async Task<IActionResult> Get()
        {
            var agents = await _db.Agents.Select(Mappers.ToAgentSummary).ToListAsync();
            return Ok(agents);
        }
        /// <summary>
        /// Gets details of a single agent by ID
        /// </summary>
        /// <param name="Id">The ID of the agent</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Agent), 200)]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var agent = await _db.Agents.Where(x=>x.Id == id).Select(Mappers.ToAgentDetails).FirstOrDefaultAsync();
            if (agent != null)
            {
                return Ok(agent);
            }
            return NotFound("No Agent with this ID found");
        }

        /// <summary>
        /// Creates a new Agent and returns the agents details once created
        /// </summary>
        /// <param name="agent">The information of the agent to be added</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Agent), 200)]
        public async Task<IActionResult> Post(Agent agent)
        {
            //handle default int values. sqlite is a bit weird about values
            agent.Id = agent.Id == 0 ? null : agent.Id;

            if (agent.Id != null)
            {
                return BadRequest("Please Submit a new agent (no agent id)");
            }

            var dbAgent = Mappers.ToDBAgent(agent);
            _db.Agents.Add(dbAgent);
            await _db.SaveChangesAsync();

            var returnAgent = await _db.Agents.Select(Mappers.ToAgentDetails).FirstAsync(x=>x.Id == dbAgent.Id);

            return Ok(returnAgent);
        }


        /// <summary>
        /// Updates an Agent
        /// </summary>
        /// <param name="id">The ID of the agent</param>
        /// <param name="agent">The agent information to update</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Agent), 200)]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id ,Agent agent)
        {
            if( id != agent.Id)
            {
                return BadRequest("ID mismatch, please submit matching Agent Ids");
            }

            var existingAgent = await _db.Agents.AnyAsync(x => x.Id == id);

            if (agent.Id == null || !existingAgent)
            {
                return BadRequest("Please submit an Existing agent");
            }

            var dbAgent = Mappers.ToDBAgent(agent);

            _db.Agents.Update(dbAgent);

            await _db.SaveChangesAsync();

            var returnAgent = await _db.Agents.Select(Mappers.ToAgentDetails).FirstAsync(x => x.Id == dbAgent.Id);


            return Ok(returnAgent);
        }
      

    }
}
