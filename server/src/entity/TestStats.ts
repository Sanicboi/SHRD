import { Column, Entity, JoinColumn, ManyToOne, OneToOne, PrimaryGeneratedColumn } from "typeorm";
import { User } from "./User";
import { Test } from "./Test";

@Entity()
export class TestStats {
    @PrimaryGeneratedColumn('uuid')
    id: string;

    @ManyToOne(() => User, (user) => user.statistics)
    user: User;

    @ManyToOne(() => Test, (test) => test.stats)
    test: Test;

    @Column('real')
    result: number;
}