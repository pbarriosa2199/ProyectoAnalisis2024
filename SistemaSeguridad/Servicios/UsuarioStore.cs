using Microsoft.AspNetCore.Identity;
using SistemaSeguridad.Models;

namespace SistemaSeguridad.Servicios
{
	public class UsuarioStore : IUserStore<UsuarioPrueba>, IUserEmailStore<UsuarioPrueba>, IUserPasswordStore<UsuarioPrueba>
	{
		private readonly IRepositoryUsuarios repositoryUsuarios;

		public UsuarioStore(IRepositoryUsuarios repositoryUsuarios)
        {
			this.repositoryUsuarios = repositoryUsuarios;
		}
        public async Task<IdentityResult> CreateAsync(UsuarioPrueba user, CancellationToken cancellationToken)
		{
			user.IdUsuario = await repositoryUsuarios.CrearUsuario(user);
			return IdentityResult.Success;
		}

		public Task<IdentityResult> DeleteAsync(UsuarioPrueba user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
		}

		public async Task<UsuarioPrueba> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
		{
			return await repositoryUsuarios.BuscarUsuarioEmail(normalizedEmail);
		}

		public async Task<UsuarioPrueba> FindByIdAsync(string userId, CancellationToken cancellationToken)
		{
			return await repositoryUsuarios.BuscarUsuarioNombre(userId);
		}

		public async Task<UsuarioPrueba> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
		{
			return await repositoryUsuarios.BuscarUsuarioNombre(normalizedUserName);
		}

		public Task<string> GetEmailAsync(UsuarioPrueba user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.CorreoElectronico);
		}

		public Task<bool> GetEmailConfirmedAsync(UsuarioPrueba user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<string> GetNormalizedEmailAsync(UsuarioPrueba user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<string> GetNormalizedUserNameAsync(UsuarioPrueba user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<string> GetPasswordHashAsync(UsuarioPrueba user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.Password);
		}

		public Task<string> GetUserIdAsync(UsuarioPrueba user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.IdUsuario);
		}

		public Task<string> GetUserNameAsync(UsuarioPrueba user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.IdUsuario);
		}

		public Task<bool> HasPasswordAsync(UsuarioPrueba user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task SetEmailAsync(UsuarioPrueba user, string email, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task SetEmailConfirmedAsync(UsuarioPrueba user, bool confirmed, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task SetNormalizedEmailAsync(UsuarioPrueba user, string normalizedEmail, CancellationToken cancellationToken)
		{
			user.CorreoElectronico = normalizedEmail;
			return Task.CompletedTask;
		}

		public Task SetNormalizedUserNameAsync(UsuarioPrueba user, string normalizedName, CancellationToken cancellationToken)
		{
			user.Nombre = normalizedName;
			return Task.CompletedTask;
		}

		public Task SetPasswordHashAsync(UsuarioPrueba user, string passwordHash, CancellationToken cancellationToken)
		{
			user.Password = passwordHash;
			return Task.CompletedTask;
		}

		public Task SetUserNameAsync(UsuarioPrueba user, string userName, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IdentityResult> UpdateAsync(UsuarioPrueba user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
