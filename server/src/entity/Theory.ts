import { Column, Entity, ManyToOne, PrimaryGeneratedColumn } from "typeorm";
import { User } from "./User";


@Entity()
export class Theory {

    @PrimaryGeneratedColumn('uuid')
    id: string;

    @Column()
    name: string;

    @Column('text')
    text: string;

    @Column()
    course: 'algebra' | 'geometry' | 'statistics';

    @ManyToOne(() => User, (user) => user.theory)
    user: User;
}