import { Entity, PrimaryGeneratedColumn, Column, OneToMany } from "typeorm"
import { Test } from "./Test";
import { TestStats } from "./TestStats";
import { Theory } from "./Theory";

@Entity()
export class User {

    @PrimaryGeneratedColumn('uuid')
    id: string;

    @Column({unique: true})
    username: string;


    @Column()
    password: string;

    @OneToMany(() => Test, (test) => test.user)
    tests: Test[];

    @OneToMany(() => TestStats, (stats) => stats.user)
    statistics: TestStats[];

    @OneToMany(() => Theory, (theory) => theory.user)
    theory: Theory[];
}
