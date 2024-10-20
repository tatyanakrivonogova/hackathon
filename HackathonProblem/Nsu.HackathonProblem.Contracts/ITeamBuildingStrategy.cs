namespace Nsu.HackathonProblem.Contracts
{
    public interface ITeamBuildingStrategy
    {
        /// <summary>
        /// Распределяет тимлидов и джунов по командам
        /// </summary>
        /// <param name="teamLeads">Тимлиды</param>
        /// <param name="juniors">Джуны</param>
        /// <returns>Список команд</returns>
        IEnumerable<Team> BuildTeams(IEnumerable<TeamLead> teamLeads, IEnumerable<Junior> juniors,
            IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists);
    }
}
