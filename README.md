# Reto_DCXAir

Resumen del Proyecto:
Este proyecto se desarrolló con un enfoque sólido en factores técnicos clave para garantizar la mantenibilidad y escalabilidad del sistema. Destaca la capacidad de persistencia de datos, ya que al realizar búsquedas de nuevas rutas, el sistema puede almacenar información en bases de datos para reducir la dependencia de fuentes externas y mejorar la velocidad de consulta.

La arquitectura del sistema sigue el modelo de arquitectura en capas (N-tier) delineado durante la fase de requisitos. Este enfoque tiene como objetivo desacoplar funcionalidades y asignar a cada capa una responsabilidad única, lo que resulta en un código más limpio y escalable siguiendo los principios SOLID.

Algoritmos de Búsqueda:
Para cumplir con los requisitos del proyecto, se utilizaron algoritmos de búsqueda. Específicamente, se implementó la Búsqueda en Anchura (BFS) para recorrer las rutas de vuelo a través de una estructura de gráfico y generar respuestas precisas. Si bien se consideró la Búsqueda en Profundidad (DFS), su tendencia a priorizar rutas más largas lo hizo menos adecuado para la recuperación clara de información.

Patrones y Prácticas de Diseño:
Se implementaron patrones de diseño como Mediator y Repository para el manejo organizado de datos. El patrón Repository garantiza un manejo estructurado de los datos, mientras que el patrón Mediator facilita la comunicación entre entidades asociadas, como los controladores. Además, se integraron la inyección de dependencias, la configuración CORS, el registro y el manejo de excepciones para un comportamiento robusto del sistema.

Calidad del Código y Desacoplamiento:
El sistema prioriza el código desacoplado al segregar funcionalidades en métodos o servicios independientes. Este enfoque mejora la modularidad y facilita el mantenimiento y las actualizaciones.

Arquitectura del Frontend:
La arquitectura del frontend está basada en componentes, organizada en torno a características o funcionalidades compartidas. Se aprovechan los observables, las suscripciones y los servicios para establecer comunicación entre componentes y garantizar un intercambio de datos sin problemas. Se incorporan las bibliotecas Bootstrap y jQuery para mejorar la dinámica y estética de la página, mientras que las validaciones de formularios garantizan una funcionalidad óptima.

Cronograma del Proyecto:
Se dedicaron aproximadamente 30 horas al proyecto, con 5 horas para estudio y planificación, 5 para desarrollo frontend y las 20 restantes para desarrollo backend. La fase de desarrollo backend fue extensa debido a la implementación de principios de arquitectura limpia y el cumplimiento de los fundamentos del código limpio.

Áreas de Mejora:
Debido a limitaciones de tiempo, ciertas características podrían haberse optimizado aún más, como la inyección de dependencias personalizadas y la configuración CORS basada en el origen.

Acceso a repositorio: https://github.com/JmBeltranCardona/Reto_DCXAir

Recomendaciones:
Base de Datos en SQLite:
La base de datos utilizada se encuentra en la capa de Infraestructura - Database y está implementada con SQLite. Se recomienda abrir la base de datos en DBeaver u otro gestor de bases de datos compatible. Además, se sugiere crear una migración y cargarla. Todo el proceso se realiza mediante EntityFramework.

API de Conversión de Precios:
Es importante tener en cuenta que el uso de la API de conversión de precios está limitado debido a su naturaleza gratuita. Aunque la API ofrece más de mil intentos, se debe utilizar con prudencia para evitar exceder los límites de uso.

Tecnologías utilizadas: C#, .Net 8 - SqlLite y Angular 15

