﻿using api_plantsp.Models;

namespace api_plantsp.Repository.Contract
{
    public interface IEnderecoRepository
    {
        Endereco Cadastrar(Endereco endereco);
        void Atualizar(Endereco endereco);
        Endereco ObterEndereco(int Id);
    }
}
