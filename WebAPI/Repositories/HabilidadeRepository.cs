using Microsoft.EntityFrameworkCore;
using DbLayer.Context;
using DbLayer.Models;
using WebAPI.DTOClasses;
using WebAPI.Interfaces;

namespace WebAPI.Repositories
{
    public class HabilidadeRepository : IHabilidadeRepository
    {
        private readonly ApplicationDbContext _context;

        public HabilidadeRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
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
            
            var categoriaExiste = await _context.CategoriasProfissionais.AnyAsync(c => c.Categoriaid == dto.Categoriaid);
            if (!categoriaExiste)
            {
                throw new ArgumentException("Categoria não encontrada.");
            }

            var criadorExiste = await _context.Utilizadores.AnyAsync(u => u.Utilizadorid == dto.Criadorid);
            if (!criadorExiste)
            {
                throw new ArgumentException("Criador não encontrado.");
            }

            var habilidade = new Habilidade
            {
                Nome = dto.Nome,
                Categoriaid = dto.Categoriaid,
                Criadorid = dto.Criadorid
            };

            _context.Habilidades.Add(habilidade);
            await _context.SaveChangesAsync();
            
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
            
            var categoriaExiste = await _context.CategoriasProfissionais.AnyAsync(c => c.Categoriaid == dto.Categoriaid);
            if (!categoriaExiste)
            {
                throw new ArgumentException("Categoria não encontrada.");
            }          
        
            var criadorIdOriginal = habilidade.Criadorid;
            
            habilidade.Nome = dto.Nome;
            habilidade.Categoriaid = dto.Categoriaid;
            habilidade.Criadorid = criadorIdOriginal;  

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
                .Include(h => h.PropostasHabilidades)
                .FirstOrDefaultAsync(h => h.Habilidadeid == id);

            if (habilidade == null)
                return false;

            if (habilidade.PropostasHabilidades.Any())
                return false;
            
            if (habilidade.TalentosHabilidades.Any())
                return false;

            _context.Habilidades.Remove(habilidade);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> HabilidadeExistsAsync(int id)
        {
            return await _context.Habilidades.AnyAsync(h => h.Habilidadeid == id);
        }
    }
}