﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Entities;
using Mealmate.Core.Interfaces;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories;
using Mealmate.Core.Specifications;
using Mealmate.Infrastructure.Paging;

namespace Mealmate.Application.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IAppLogger<MenuItemService> _logger;
        private readonly IMapper _mapper;

        public MenuItemService(
            IMenuItemRepository menuItemRepository, 
            IAppLogger<MenuItemService> logger, 
            IMapper mapper)
        {
            _menuItemRepository = menuItemRepository ?? throw new ArgumentNullException(nameof(menuItemRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<MenuItemModel> Create(MenuItemModel model)
        {
            var existingMenuItem = await _menuItemRepository.GetByIdAsync(model.Id);
            if (existingMenuItem != null)
            {
                throw new ApplicationException("menuItem with this id already exists");
            }

            var newmenuItem = _mapper.Map<MenuItem>(model);
            newmenuItem = await _menuItemRepository.SaveAsync(newmenuItem);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newmenuItemmodel = _mapper.Map<MenuItemModel>(newmenuItem);
            return newmenuItemmodel;
        }

        public async Task Delete(int id)
        {
            var existingMenuItem = await _menuItemRepository.GetByIdAsync(id);
            if (existingMenuItem == null)
            {
                throw new ApplicationException("MenuItem with this id is not exists");
            }

            await _menuItemRepository.DeleteAsync(existingMenuItem);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<MenuItemModel>> Get(int menuId)
        {
            var result = await _menuItemRepository.GetAsync(x => x.MenuId== menuId);
            return _mapper.Map<IEnumerable<MenuItemModel>>(result);
        }

        public async Task<MenuItemModel> GetById(int id)
        {
            return _mapper.Map<MenuItemModel>(await _menuItemRepository.GetByIdAsync(id));
        }

        public async Task Update(MenuItemModel model)
        {
            var existingMenuItem = await _menuItemRepository.GetByIdAsync(model.Id);
            if (existingMenuItem == null)
            {
                throw new ApplicationException("MenuItem with this id is not exists");
            }

            existingMenuItem = _mapper.Map<MenuItem>(model);

            await _menuItemRepository.SaveAsync(existingMenuItem);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        //public async Task<IEnumerable<MenuItemModel>> GetMenuItemList()
        //{
        //    var MenuItemList = await _menuItemRepository.ListAllAsync();

        //    var MenuItemModels = ObjectMapper.Mapper.Map<IEnumerable<MenuItemModel>>(MenuItemList);

        //    return MenuItemModels;
        //}

        //public async Task<IPagedList<MenuItemModel>> SearchMenuItems(PageSearchArgs args)
        //{
        //    var MenuItemPagedList = await _menuItemRepository.SearchMenuItemsAsync(args);

        //    //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
        //    var MenuItemModels = ObjectMapper.Mapper.Map<List<MenuItemModel>>(MenuItemPagedList.Items);

        //    var MenuItemModelPagedList = new PagedList<MenuItemModel>(
        //        MenuItemPagedList.PageIndex,
        //        MenuItemPagedList.PageSize,
        //        MenuItemPagedList.TotalCount,
        //        MenuItemPagedList.TotalPages,
        //        MenuItemModels);

        //    return MenuItemModelPagedList;
        //}

        //public async Task<MenuItemModel> GetMenuItemById(int MenuItemId)
        //{
        //    var MenuItem = await _menuItemRepository.GetByIdAsync(MenuItemId);

        //    var MenuItemModel = ObjectMapper.Mapper.Map<MenuItemModel>(MenuItem);

        //    return MenuItemModel;
        //}

        //public async Task<IEnumerable<MenuItemModel>> GetMenuItemsByName(string name)
        //{
        //    var spec = new MenuItemWithMenuItemesSpecification(name);
        //    var MenuItemList = await _menuItemRepository.GetAsync(spec);

        //    var MenuItemModels = ObjectMapper.Mapper.Map<IEnumerable<MenuItemModel>>(MenuItemList);

        //    return MenuItemModels;
        //}

        //public async Task<IEnumerable<MenuItemModel>> GetMenuItemsByCategoryId(int categoryId)
        //{
        //    var spec = new MenuItemWithMenuItemesSpecification(categoryId);
        //    var MenuItemList = await _menuItemRepository.GetAsync(spec);

        //    var MenuItemModels = ObjectMapper.Mapper.Map<IEnumerable<MenuItemModel>>(MenuItemList);

        //    return MenuItemModels;
        //}

        //public async Task<MenuItemModel> CreateMenuItem(MenuItemModel MenuItem)
        //{
        //    var existingMenuItem = await _menuItemRepository.GetByIdAsync(MenuItem.Id);
        //    if (existingMenuItem != null)
        //    {
        //        throw new ApplicationException("MenuItem with this id already exists");
        //    }

        //    var newMenuItem = ObjectMapper.Mapper.Map<MenuItem>(MenuItem);
        //    newMenuItem = await _menuItemRepository.SaveAsync(newMenuItem);

        //    _logger.LogInformation("Entity successfully added - MealmateAppService");

        //    var newMenuItemModel = ObjectMapper.Mapper.Map<MenuItemModel>(newMenuItem);
        //    return newMenuItemModel;
        //}

        //public async Task UpdateMenuItem(MenuItemModel MenuItem)
        //{
        //    var existingMenuItem = await _menuItemRepository.GetByIdAsync(MenuItem.Id);
        //    if (existingMenuItem == null)
        //    {
        //        throw new ApplicationException("MenuItem with this id is not exists");
        //    }

        //    existingMenuItem.Name = MenuItem.Name;
        //    existingMenuItem.Description = MenuItem.Description;

        //    await _menuItemRepository.SaveAsync(existingMenuItem);

        //    _logger.LogInformation("Entity successfully updated - MealmateAppService");
        //}

        //public async Task DeleteMenuItemById(int MenuItemId)
        //{
        //    var existingMenuItem = await _menuItemRepository.GetByIdAsync(MenuItemId);
        //    if (existingMenuItem == null)
        //    {
        //        throw new ApplicationException("MenuItem with this id is not exists");
        //    }

        //    await _menuItemRepository.DeleteAsync(existingMenuItem);

        //    _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        //}
    }
}
