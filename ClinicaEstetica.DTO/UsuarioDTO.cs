namespace ClinicaEstetica.DTO
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Status { get; set; }
        public int TipoUsuario { get; set; }


        //public DateTime DataCadastro { get; set; }
        // Outros campos relevantes para o usuário
    }
}
