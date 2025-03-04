﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Areas.Admin.Models
{
    public class Pager
    {
        public Pager(int totalItems, int? page, int pageSize = 5)//khởi tạo pager(trang)
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 3;
            var endPage = currentPage + 2;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 5)
                {
                    startPage = endPage - 4;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        public int TotalItems { get; private set; }//tổng số trang
        public int CurrentPage { get; private set; }//trang hiện tại
        public int PageSize { get; private set; }//kích thước trang
        public int TotalPages { get; private set; }//tổng số trang
        public int StartPage { get; private set; }//trang bắt đầu
        public int EndPage { get; private set; }//trang kết thúc
    }
}