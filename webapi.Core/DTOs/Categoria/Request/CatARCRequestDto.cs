using System;
using System.Collections.Generic;
using System.Text;

namespace webapi.Core.DTOs.Categoria.Request
{
    /// <summary>
    /// This dto is for the collection of <see cref="ArticuloAddRequestDto"/>
    /// that represents relation article-category.
    /// </summary>
    public sealed class CatARCRequestDto
    {
        public int Id { get; set; }
    }
}
