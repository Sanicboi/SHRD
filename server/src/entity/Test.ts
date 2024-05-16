import { Column, Entity, ManyToOne, OneToMany, PrimaryGeneratedColumn } from "typeorm";
import { Task } from "./Task";
import { User } from "./User";
import { TestStats } from "./TestStats";


@Entity()
export class Test {
    @PrimaryGeneratedColumn('uuid')
    id: string;

    @Column()
    name: string;

    @Column()
    course: 'algebra' | 'geometry' | 'statistics';


    @OneToMany(() => Task, (task) => task.test)
    tasks: Task[];

    @ManyToOne(() => User, (user) => user.tests)
    user: User;

    @OneToMany(() => TestStats, (stats) => stats.test)
    stats: TestStats[];
}