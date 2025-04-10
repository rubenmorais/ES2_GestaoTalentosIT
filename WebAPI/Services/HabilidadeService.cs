using Microsoft.EntityFrameworkCore;
using DbLayer.Context;
using DbLayer.Models;
using WebAPI.DTOClasses;

namespace WebAPI.Services
{
    public class HabilidadeService 
    {
        private readonly ApplicationDbContext _context;

        public HabilidadeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HabilidadeDTO>> GetAllAsync()
        {
            var habilidades = await _context.Habilidades.ToListAsync();
            return habilidades.Select(h => new HabilidadeDTO
            {
                Habilidadeid = h.Habilidadeid,
                Nome = h.Nome,
                Categoriaid = h.Categoriaid,
                Criadorid = h.Criadorid
            });
        }

        public async Task<HabilidadeDTO?> GetByIdAsync(int id)
        {
            var habilidade = await _context.Habilidades.FirstOrDefaultAsync(h => h.Habilidadeid == id);
            if (habilidade is null)
                return null;

            return new HabilidadeDTO
            {
                Habilidadeid = habilidade.Habilidadeid,
                Nome = habilidade.Nome,
                Categoriaid = habilidade.Categoriaid,
                Criadorid = habilidade.Criadorid
            };
        }

        public async Task<HabilidadeDTO> CreateAsync(CreateHabilidadeDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
            {
                throw new ArgumentException("Nome da habilidade é obrigatório.", nameof(dto.Nome));
            }

            var habilidade = new Habilidade
            {
                Nome = dto.Nome,
                Categoriaid = dto.Categoriaid,
                Criadorid = dto.Criadorid
            };

            _context.Habilidades.Add(habilidade);
            await _context.SaveChangesAsync();

            // Retorna um DTO contendo o ID gerado e os dados salvos
            return new HabilidadeDTO
            {
                Habilidadeid = habilidade.Habilidadeid,
                Nome = habilidade.Nome,
                Categoriaid = habilidade.Categoriaid,
                Criadorid = habilidade.Criadorid
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateHabilidadeDTO dto)
        {
            var habilidade = await _context.Habilidades.FindAsync(id);
            if (habilidade == null)
                return false;

            // Atualiza os dados da habilidade
            habilidade.Nome = dto.Nome;
            habilidade.Categoriaid = dto.Categoriaid;
            habilidade.Criadorid = dto.Criadorid;

            _context.Entry(habilidade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HabilidadeExistsAsync(id))
                    return false;
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var habilidade = await _context.Habilidades
                .Include(h => h.TalentosHabilidades)
                .FirstOrDefaultAsync(h => h.Habilidadeid == id);

            if (habilidade == null)
                return false;

            // Exemplo: Não apagar se existir relacionamento
            if (habilidade.TalentosHabilidades.Any())
                return false;

            _context.Habilidades.Remove(habilidade);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> HabilidadeExistsAsync(int id)
        {
            return await _context.Habilidades.AnyAsync(h => h.Habilidadeid == id);
        }
    }
}
