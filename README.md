# Escuela ACME - Gestión de Cursos y Estudiantes - Cristian Bisbicus

## Descripción
Este proyecto es una solución sencilla para gestionar estudiantes, cursos y pagos. La solución proporciona funcionalidades básicas para:

- Registrar estudiantes adultos (18 años o más).
- Registrar cursos con nombre, cuota de inscripción y fechas de inicio y finalización.
- Permitir que un estudiante se inscriba en un curso luego de realizar el pago de la cuota.
- Consultar los cursos y estudiantes inscritos dentro de un rango de fechas.

La implementación está basada en C# con un enfoque de buenas prácticas y una arquitectura flexible, que permite futuras expansiones sin requerir una reescritura completa del código.

## Decisiones Tomadas

### 1. Modelo de Dominio
Se decidió usar un enfoque simple pero extensible en la estructura del dominio. Las clases `Student` (Estudiante) y `Course` (Curso) contienen las propiedades y métodos básicos para representar a estos elementos en el sistema.
Los métodos en la clase `SchoolDomain` gestionan las interacciones de negocio, como registrar estudiantes, registrar cursos e inscribir estudiantes en los cursos.

### 2. Asincronía
Se eligió implementar operaciones asíncronas para que las acciones de registro e inscripción, que involucran acceso a repositorios y pasarelas de pago, no bloqueen el hilo principal de la aplicación. Esto es crucial para mejorar la escalabilidad y la capacidad de respuesta del sistema, especialmente cuando se integren servicios externos como APIs de pago o bases de datos.

### 3. Validaciones
Se implementaron validaciones básicas para los estudiantes (mayoría de edad) y los cursos (validez de fechas, cuota de inscripción, nombre no vacío). Esto asegura que no se registren datos erróneos o inconsistentes en el sistema.

### 4. Interfaces para Abstracción
El uso de interfaces como `ISchoolRepository` y `IPaymentGateway` permite que el sistema sea fácilmente extensible. Esto significa que en el futuro podríamos cambiar la implementación de la pasarela de pagos o la persistencia de los datos sin afectar la lógica del dominio. Esto es clave para la evolución del sistema, por ejemplo, para integrar una base de datos real o una pasarela de pago como Stripe.

### 5. Respuestas Genéricas
El sistema utiliza un objeto `Response<T>` para envolver todas las respuestas de las operaciones, lo que permite devolver no solo el resultado, sino también mensajes de error o éxito de manera estructurada y consistente.

## Cosas que me hubiera gustado hacer pero no hice en la prueba

1. **Persistencia de Datos**: No se implementó una base de datos para almacenar la información de los estudiantes y cursos, pero sería una extensión natural del proyecto.
2. **Interfaz de Usuario**: Aunque la solución es funcional, no tiene una interfaz gráfica ni una API. Una API RESTful sería el siguiente paso para que el sistema sea más accesible.
3. **Autenticación y Autorización**: En un sistema real, sería necesario implementar autenticación para asegurar que solo usuarios autorizados puedan realizar ciertas acciones, como inscribir estudiantes o gestionar cursos.

## Cosas que podría mejorar

1. **Manejo de Errores**: El sistema podría beneficiarse de un manejo de errores más detallado, especialmente en la interacción con servicios externos como las pasarelas de pago.
2. **Pruebas**: Se podrían agregar más pruebas, especialmente en la parte de validación de fechas y los estados de los pagos.

## Bibliotecas de Terceros Utilizadas

1. **xUnit.net**: Para la ejecución de pruebas unitarias.
2. **Moq (opcional)**: Si se quisiera simular interacciones más complejas, se podría usar Moq para crear mocks de las interfaces.
3. **AutoMapper**: mapeo de objetos entre las diferentes capas

## Tiempo de Desarrollo

Este proyecto fue completado en aproximadamente **8 horas**. Las principales tareas fueron la implementación del modelo de dominio, la creación de pruebas unitarias y la estructura básica del proyecto. 
Tuve que investigar y analizar algunos escenarios para aplicar sobre **xUnit.net** y aprender más sobre las mejores prácticas en pruebas unitarias.
