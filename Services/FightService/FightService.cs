using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.DTOs.Fight;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FightService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<FightResultDTO>> Fight(FightRequestDTO request)
        {
            var response = new ServiceResponse<FightResultDTO>()
            {
                Data = new FightResultDTO()
            };

            try
            {
                var characters = await _context.Characters
                    .Include(x => x.Weapon)
                    .Include(x => x.Skills)
                    .Where(c => request.CharacterIDs.Contains(c.Id))
                    .ToListAsync();


                bool isDefeated = false;


                while(isDefeated) 
                {
                    foreach (var attacker in characters)
                    {
                        var opponents = characters.Where(c => c.Id != attacker.Id).ToList();
                        var opponent = opponents[new Random().Next(opponents.Count)];

                        int damage = 0;
                        string attackUsed = string.Empty;


                        bool useWeapon = new Random().Next(2) == 0;

                        if (useWeapon && attacker.Weapon is not null)
                        {
                            attackUsed = attacker.Weapon.Name;
                            damage = DoWeaponAttack(attacker, opponent);
                        }
                        else if (!useWeapon && attacker.Weapon is not null)
                        {
                            var skill = attacker.Skills[new Random().Next(attacker.Skills.Count())];
                            attackUsed = skill.Name!;
                            damage = DoSkillAttack(attacker, opponent,skill);
                        }
                        else
                        {
                            response.Data.Log
                                .Add($"{attacker.Name} was not able to attack!");
                            continue;
                        }


                        response.Data.Log
                            .Add($"{attacker.Name} attacks {opponent.Name} using {attackUsed} with {(damage >= 0 ? damage : 0)} damage");

                        if (opponent.HitPoint <= 0)
                        {
                            isDefeated = true;
                            attacker.Victories++;
                            opponent.Defeats++;

                            response.Data.Log.Add($"{opponent.Name} has been defeated!");
                            response.Data.Log.Add($"{attacker.Name} wins with {attacker.HitPoint} HP left!");
                        }
                    }

                }

                characters.ForEach(c =>
                {
                    c.Fights++;
                    c.HitPoint = 100;
                });

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                response.isSuccessful = false;
                response.Message = ex.Message;
            }

            return response;

        }

        public async Task<ServiceResponse<AttackResultDTO>> SkillAttack(SkillAttackDTO request)
        {
            var response = new ServiceResponse<AttackResultDTO>();

            try
            {
                var attacker = await _context.Characters
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerID);

                var opponent = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentID);


                if (attacker is null || opponent is null || attacker.Skills is null)
                    throw new Exception("Something is not properly set..!..");


                var skill = attacker.Skills.FirstOrDefault(s => s.ID == request.SkillID);
                if (skill is null)
                {
                    response.isSuccessful = false;
                    response.Message = $"{attacker.Name} does not that skill!";

                    return response;
                }

                int damage = DoSkillAttack(attacker, opponent, skill);

                if (opponent.HitPoint <= 0)
                    response.Message = $"{opponent.Name} has been defeated";

                await _context.SaveChangesAsync();


#pragma warning disable CS8601 // Possible null reference assignment. I have already checked this issue above with null-checks.
                response.Data = new AttackResultDTO()
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHP = attacker.HitPoint,
                    OpponentHP = opponent.HitPoint,
                    Damage = damage
                };
#pragma warning restore CS8601 // Possible null reference assignment.
            }
            catch (Exception ex)
            {
                response.isSuccessful = false;
                response.Message = ex.Message;
            }

            return response;
        }

        private int DoSkillAttack(Character attacker, Character opponent, Skill skill)
        {
            int damage = skill.Damage + (new Random().Next(attacker.Intelligence));
            damage -= new Random().Next(opponent.Defence);

            if (damage > 0)
                opponent.HitPoint -= damage;
            return damage;
        }

        public async Task<ServiceResponse<AttackResultDTO>> WeaponAttack(WeaponAttackDTO request)
        {
            var response = new ServiceResponse<AttackResultDTO>();

            try
            {
                var attacker = await _context.Characters
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerID);

                var opponent = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentID);


                if (attacker is null || opponent is null || attacker.Weapon is null)
                    throw new Exception("Something is not properly set..!..");

                int damage = DoWeaponAttack(attacker, opponent);



                if (opponent.HitPoint <= 0)
                    response.Message = $"{opponent.Name} has been defeated";

                await _context.SaveChangesAsync();


#pragma warning disable CS8601 // Possible null reference assignment. I have already checked this issue above with null-checks.
                response.Data = new AttackResultDTO()
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHP = attacker.HitPoint,
                    OpponentHP = opponent.HitPoint,
                    Damage = damage
                };
#pragma warning restore CS8601 // Possible null reference assignment.
            }
            catch (Exception ex)
            {
                response.isSuccessful = false;
                response.Message = ex.Message;
            }

            return response;

        }

        private int DoWeaponAttack(Character attacker, Character opponent)
        {
            if (attacker.Weapon is null)
            {
                throw new Exception("Attacker has no weapon!");
            }

            int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
            damage -= new Random().Next(opponent.Defence);

            if (damage > 0)
                opponent.HitPoint -= damage;

            return damage;
        }

        public async Task<ServiceResponse<List<HighScoreDTO>>> GetHighScore()
        {

            var characters = await _context.Characters
                .Where(c => c.Fights > 0)
                .OrderByDescending(c => c.Victories)
                .ThenBy(c => c.Defeats)
                .ToListAsync();

            var response = new ServiceResponse<List<HighScoreDTO>>()
            {
                Data = characters.Select(c => _mapper.Map<HighScoreDTO>(c)).ToList()
            };


            return response;
        }
    }
}
