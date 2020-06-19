# AMQP-based message router

This is a proof-of-concept of a simple message router infrastructure in a distributed systems architecture.

Terminology is inspired from Domain Driven Design (DDD), which is why you'll find concepts like e.g. "Bounded Context" or "Published Language".

# Design objectives

The purpose of this proof-of-concept is to:

- Evaluate AMQP as a potential brokering protocol
- Implement a standards-based solution, that is independent from the broker platform vendor
- Illustrate certain DDD concepts in a distributed systems architecture

## AMQP 0.9.1

The AMQP 0.9.1 version is based on the [EasyNetQ library](https://github.com/EasyNetQ) and the AMQP platform support is handled by [RabbitMQ](https://www.rabbitmq.com).

## AMQP 1.0

I am working on an AMQP 1.0 version as well, stay tuned :)

# How can I contribute?

That's easy, just write me on larslb@thinkability.dk or open an issue in this repo :)

All contributions are very welcome!
