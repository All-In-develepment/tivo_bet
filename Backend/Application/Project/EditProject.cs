using Application.Core;
using AutoMapper;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Project
{
    public class EditProject
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Domain.Project Project { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Project).SetValidator(new ProjectValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                // Encontra o projeto no banco de dados
                var project = await _context.Projects.FindAsync(request.Project.ProjectId);
                
                // Se o projeto não existir, retorna nulo
                if (project == null) return null;

                // Mapeia o projeto recebido para o projeto encontrado no banco de dados
                _mapper.Map(request.Project, project);
                project.ProjectName = request.Project.ProjectName;
                project.ProjectIsActive = request.Project.ProjectIsActive;
                project.ProjectDescription = request.Project.ProjectDescription;

                // Salva as alterações no banco de dados
                var result = await _context.SaveChangesAsync() > 0;

                // Se não conseguir salvar, retorna uma mensagem de erro
                if (!result) return Result<Unit>.Failure("Failed to update project");

                // Retorna uma mensagem de sucesso
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}