import "reflect-metadata"
import { DataSource } from "typeorm"
import { User } from "./entity/User"
import { Task } from "./entity/Task"
import { TestStats } from "./entity/TestStats"
import { Test } from "./entity/Test"
import { Theory } from "./entity/Theory"

export const AppDataSource = new DataSource({
    type: "postgres",
    host: "db",
    port: 5432,
    username: "root",
    password: process.env.PG_PASS,
    database: "main",
    synchronize: true,
    logging: false,
    entities: [User, Task, TestStats, Test, Theory],
    migrations: [],
    subscribers: [],
})
