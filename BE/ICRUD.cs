using System.Collections.Generic;

/// <summary>

/// ''' Interfaz que define los metodos que todas las clases que expongan mecanismos de

/// ''' Alta, Baja, Modificacion y Consulta deben implementar (Create Read Update Delete).

/// ''' </summary>

/// ''' <typeparam name="T"></typeparam>

/// ''' <remarks></remarks>
public interface ICRUD<T>
{
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
    List<T> ConsultaRango(T filtroDesde, T filtroHasta);
}


