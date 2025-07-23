# Core‑Driven Architecture

![Core‑Driven Architecture](https://imgur.com/M5A9rb0.png)

## 🌟 ¿Qué es Core‑Driven Architecture?

La **Core‑Driven Architecture** es una combinación deliberada de **Clean Architecture**, **Hexagonal Architecture** y **Layered Architecture**. Su objetivo principal es mantener **el núcleo de negocio (Core)** como el centro absoluto de la aplicación, garantizando que todas las demás capas dependan únicamente de él, sin importar detalles técnicos externos 

Este enfoque promueve una **separación clara de responsabilidades** entre:
- **Punto de entrada** (endpoints, handlers, UI): se limita a recibir solicitudes, enrutar y aplicar autorizaciones.
- **Capa de casos de uso**: contiene la lógica de negocio central, implementando un solo caso por clase y manejando validaciones, reglas, persistencia e incluso publicación de eventos de forma coordinada
- **Elementos externos** (infraestructura, repositorios, envío de eventos, I/O): inyectados mediante interfaces, siguiendo el estilo de **puertos y adaptadores** de la arquitectura hexagonal
### Beneficios clave
- **Desacoplamiento extremo**: el core no depende de frameworks, librerías o infraestructura.
- **Alta testabilidad**: los casos de uso se pueden probar aislados con mocks o fakes.
- **Facilidad para seguir principios SOLID**: especialmente el de responsabilidad única (cada caso de uso cumple una función concreta).
- **Compatibilidad con CQRS**: gracias a la separación natural entre escritura (use cases) y lectura (queries)

---

## ✅ Estructura típica por capas

| Capa | Función principal |
|------|-------------------|
| **Entrada (API/UI/Handlers)** | Recibir solicitudes, enrutar y autorizar |
| **Casos de uso** | Lógica de negocio, validaciones, persistencia y eventos |
| **Adaptadores externos** | Base de datos, envío de eventos, frameworks, I/O |

---

## 🛠 Buenas prácticas destacadas

- Un caso de uso = una clase → claridad y cumplimiento del principio de responsabilidad única 
- No se necesita MediatR u otros mediadores; basta con inyectar directamente los casos de uso como dependencias 
- Usar interfaces solo en la capa de infraestructura, evitando sobrecargar los casos de uso con abstracciones innecesarias 
- Facilita la adopción del patrón **Result<T>** para manejar errores de forma consistente y transparente 
---

## ✅ Testabilidad

Gracias a la estricta delimitación del core y su capacidad de recibir implementaciones externas mockeadas, los casos de uso son altamente **unit‑testables**, enfocándose en los caminos felices sin necesidad de infraestructura real 

---

Con esta base ya puedes adaptar el enfoque para APIs, microservicios, arquitecturas event-driven o cualquier otro tipo de entrada, manteniendo siempre el core como el cerebro de tu aplicación.

---

## 👏 Créditos

Este resumen fue elaborado a partir del excelente contenido compartido por **NetMentor** en su blog:  
🔗 [https://www.netmentor.es/entrada/core-driven-architecture](https://www.netmentor.es/entrada/core-driven-architecture)
