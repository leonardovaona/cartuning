/// <summary>
/// ''' Interfaz que define los metodos que todas las clases que expongan mecanismos de
/// ''' persistencia de datos a un origen de datos deben implementar.
/// ''' </summary>
/// ''' <remarks></remarks>
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

public interface IMapeador<T>
{

    /// <summary>
    ///     ''' Establece u obtiene el objeto que actuara como encapsulador de la funcionalidad
    ///     ''' de acceso a datos. Si no se especifica (Nothing) se usara el predeterminado.
    ///     ''' </summary>
    ///     ''' <value></value>
    ///     ''' <returns></returns>
    ///     ''' <remarks></remarks>
    IComando Wrapper { get; set; }

    /// <summary>
    ///     ''' Establece u obtiene el objeto que actuara como conversor de los datos obtenidos
    ///     ''' desde el origen de datos. Si no se especifica (Nothing) se usara el predeterminado.
    ///     ''' </summary>
    ///     ''' <value></value>
    ///     ''' <returns></returns>
    ///     ''' <remarks></remarks>
    IConversor<T> Conversor { get; set; }

    /// <summary>
    ///     ''' Agrega un objeto del tipo T.
    ///     ''' </summary>
    ///     ''' <param name="value"></param>
    ///     ''' <returns></returns>
    ///     ''' <remarks></remarks>
    bool Alta(ref T value);

    /// <summary>
    ///     ''' Elimina un objeto del tipo T ya existente.
    ///     ''' </summary>
    ///     ''' <param name="value"></param>
    ///     ''' <returns></returns>
    ///     ''' <remarks></remarks>
    bool Baja(ref T value);

    /// <summary>
    ///     ''' Modifica un objeto del tipo T ya existente.
    ///     ''' </summary>
    ///     ''' <param name="value"></param>
    ///     ''' <returns></returns>
    ///     ''' <remarks></remarks>
    bool Modificacion(ref T value);

    /// <summary>
    ///     ''' Retorna el primer objeto del tipo T que coincida con el filtro especificado.
    ///     ''' </summary>
    ///     ''' <param name="filtro"></param>
    ///     ''' <returns></returns>
    ///     ''' <remarks></remarks>
    T Consulta(ref T filtro);

    /// <summary>
    ///     ''' Retorna todos los objetos del tipo T que coincidan con los valores
    ///     ''' especificados en el rango desde-hasta.
    ///     ''' </summary>
    ///     ''' <param name="filtroDesde"></param>
    ///     ''' <param name="filtroHasta"></param>
    ///     ''' <returns></returns>
    ///     ''' <remarks></remarks>
    List<T> ConsultaRango(ref T filtroDesde, ref T filtroHasta);
    /// <summary>
    ///     ''' Retorna todos los objetos del tipo T que coincidan con los valores
    ///     ''' especificados en el rango desde-hasta.
    ///     ''' </summary>
    ///     ''' <param name="filtroDesde"></param>
    ///     ''' <param name="filtroHasta"></param>
    ///     ''' <returns></returns>
    ///     ''' <remarks></remarks>
    List<T> ConsultaRango( T filtroDesde,  T filtroHasta);
}
