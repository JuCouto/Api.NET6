using ApiDotNet6.Application.DTOs;
using ApiDotNet6.Application.DTOs.Validations;
using ApiDotNet6.Application.Services.Interfaces;
using ApiDotNet6.Domain.Entities;
using ApiDotNet6.Domain.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IProductRepository _productRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseService(IProductRepository productRepository, IPersonRepository personRepository, IPurchaseRepository purchaseRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _personRepository = personRepository;
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<PurchaseDTO>> CreateAsync(PurchaseDTO purchaseDTO)
        {
            if (purchaseDTO == null)
                return ResultService.Fail<PurchaseDTO>("Objeto deve ser informado");

            var validate = new PurchaseDTOValidator().Validate(purchaseDTO);
            if (!validate.IsValid)
                return ResultService.RequestError<PurchaseDTO>("Problemas de validação", validate);

            // Criou o try catch para iniciar a transação e caso dê erro vai para o catch e finaliza.
            try
            {
                // Inicia a transação.
                await _unitOfWork.BeginTransaction();
                var productId = await _productRepository.GetIdByCodErpAsync(purchaseDTO.CodErp);
                // Validação para a transação, caso não encontre o produto, ele será cadastrado.
                if (productId == 0)
                {
                    var product = new Product(purchaseDTO.ProductName, purchaseDTO.CodErp, purchaseDTO.Price);
                    await _productRepository.CreateAsync(product);
                    productId = product.Id;
                }

                var personId = await _personRepository.GetIdByDocumentAsync(purchaseDTO.Document);
                var purchase = new Purchase(productId, personId);

                var data = await _purchaseRepository.CreateAsync(purchase);
                purchaseDTO.Id = data.Id;
                await _unitOfWork.Commit();
                return ResultService.Ok<PurchaseDTO>(purchaseDTO);
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollBack();
                return ResultService.Fail<PurchaseDTO>($"{ex.Message}");
            }
        }

        public async Task<ResultService<ICollection<PurchaseDetailDTO>>> GetAsync()
        {
            var purchases = await _purchaseRepository.GetAllAsync();
            return ResultService.Ok(_mapper.Map<ICollection<PurchaseDetailDTO>>(purchases));
        }

        public async Task<ResultService<PurchaseDetailDTO>> GetByIdAsync(int id)
        {
            var purchase = await _purchaseRepository.GetByIdAsync(id);
            if (purchase == null)
                return ResultService.Fail<PurchaseDetailDTO>("Compra não encontrada");

            // O mapper vai fazer a conversão customizada(Classe DomainToDto).
            return ResultService.Ok(_mapper.Map<PurchaseDetailDTO>(purchase));
        }

        public async Task<ResultService> RemoveAsync(int id)
        {
            var purchase = await _purchaseRepository.GetByIdAsync(id);

            if (purchase == null)
                return ResultService.Fail("Produto não encontrado");

            await _purchaseRepository.DeleteAsync(purchase);
            return ResultService.Ok($"Compra:{id} foi deletada.");
        }

        public async Task<ResultService<PurchaseDTO>> UpdateAsync(PurchaseDTO purchaseDTO)
        {
            if (purchaseDTO == null)
                return ResultService.Fail<PurchaseDTO>("Objeto deve ser informado");

            var validation = new PurchaseDTOValidator().Validate(purchaseDTO);
            if (!validation.IsValid)
                return ResultService.RequestError<PurchaseDTO>("Problemas de validação", validation);

            var purchase = await _purchaseRepository.GetByIdAsync(purchaseDTO.Id);
            if (purchase == null)
                return ResultService.Fail<PurchaseDTO>("Comprao não encontrada.");

            var productId = await _productRepository.GetIdByCodErpAsync(purchaseDTO.CodErp);
            var personId = await _personRepository.GetIdByDocumentAsync(purchaseDTO.Document);

            purchase.Edit(purchase.Id, productId, personId);
            await _purchaseRepository.EditAsync(purchase);
            return ResultService.Ok(purchaseDTO);
        }
    }
}
