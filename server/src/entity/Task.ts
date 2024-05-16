import { Column, Entity, ManyToOne, PrimaryGeneratedColumn } from "typeorm";
import { Test } from "./Test";

@Entity()
export class Task {
    @PrimaryGeneratedColumn('uuid')
    id: string;

    @Column()
    text: string;

    @Column()
    answer: string;

    @ManyToOne(() => Test, (test) => test.tasks)
    test: Test;
}